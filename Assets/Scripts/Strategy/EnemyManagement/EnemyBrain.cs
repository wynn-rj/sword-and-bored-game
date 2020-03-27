using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.Movement.EnemyMovementStrategies;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.Squads;
using SwordAndBored.Strategy.TimeSystem.Subscribers;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Strategy.Transitions;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Utilities.Random;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SwordAndBored.Strategy.EnemyManagement
{
    class EnemyBrain : MainThreadPreTimeStepSubscriber
    {
        [Serializable]
        private enum Behaviour
        {
            Aggresive,
            Passive,
            Defensive,
            Loading
        }

        private const float PRODUCTIVITY_DIRECTION_BOUND = 0.2f;

        private readonly Dictionary<Behaviour, int> guardDistance = new Dictionary<Behaviour, int>()
        {
            { Behaviour.Aggresive, 8 },
            { Behaviour.Passive, 4 },
            { Behaviour.Defensive, 2 },
        };

        private readonly Dictionary<Behaviour, float> protectedLocationsPercents = new Dictionary<Behaviour, float>()
        {
            { Behaviour.Aggresive, 0.1f },
            { Behaviour.Passive, 0.5f },
            { Behaviour.Defensive, 0.9f },
        };

        public float Mood
        {
            get => mood;
            set
            {
                mood = Mathf.Clamp(value, -1, 1);
                updateBehaviour = true;
            }
        }

        private float Productivity
        {
            get => productivity;
            set => productivity = Mathf.Clamp(value, -1f, 1f);
        }

        private float ProductivityDirection
        {
            get => productivityDirection;
            set => productivityDirection = Mathf.Clamp(value, -PRODUCTIVITY_DIRECTION_BOUND, PRODUCTIVITY_DIRECTION_BOUND);
        }

        public IList<EnemyMovementController> Enemies = new List<EnemyMovementController>();
        public IList<IHexGridCell> ImportantLocations = new List<IHexGridCell>();
        public IList<IHexGridCell> TargetLocations = new List<IHexGridCell>();

        [SerializeField] private EnemyMovementController enemyPrefab;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private AbstractTimeManager turnManager;
        [SerializeField] private SquadManager squadManager;
        [SerializeField] private float enemyPlacementHeight = 0;
        [SerializeField] private Behaviour currentBehaviour = Behaviour.Loading;
        [SerializeField] [Range(-1, 1)] private float mood;
        [SerializeField] [Range(-1, 1)] private float productivity;
        [SerializeField] [Range(-PRODUCTIVITY_DIRECTION_BOUND, PRODUCTIVITY_DIRECTION_BOUND)] private float productivityDirection = 0;
        private int chaserCount = 0;
        protected bool updateBehaviour = false;

        protected override void MainThreadPreTimeStepUpdate()
        {
            Mood += 0.05f;
            int desiredEnemyCount = ImportantLocations.Count + TargetLocations.Count + squadManager.squads.Count + Mathf.RoundToInt(0.08f * turnManager.TimeStep);
            ProductivityDirection += 0.001f * (desiredEnemyCount - Enemies.Count);
            Productivity += ProductivityDirection;

            if (Productivity > 0.5f && Odds.DiceRoll(5) == 1)
            {
                PlaceEnemy();
                Productivity -= 0.3f;
            }
            if (ProductivityDirection < 0)
            {
                Mood += 0.03f;
            }
        }

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(enemyPrefab, this);
            AssertHelper.IsSetInEditor(tileManager, this);
            AssertHelper.IsSetInEditor(turnManager, this);
            AssertHelper.IsSetInEditor(squadManager, this);
        }
#endif

        void Start()
        {
            ImportantLocations.Add(tileManager.EnemyBase);
            TargetLocations.Add(tileManager.PlayerBase);
            foreach (IEnemySquad enemySquad in EnemySquad.GetAllEnemySquads())
            {
                PlaceEnemy(enemySquad);
            }
            if (Enemies.Count == 0 && turnManager.TimeStep == 0)
            {
                foreach (IHexGridCell neighbor in tileManager.EnemyBase.Neighbors)
                {
                    PlaceEnemy(neighbor);
                }
            }

            foreach (ITown town in Town.GetAllTowns())
            {
                if (town.ID == 0) continue;
                Point<int> townLocation = new Point<int>(town.X, town.Y);
                if (town.PlayerOwned)
                {
                    TargetLocations.Add(tileManager.HexTiling[townLocation]);
                }
                else
                {
                    ImportantLocations.Add(tileManager.HexTiling[townLocation]);
                }
                Debug.Log(string.Format("Town at {0} flagged as {1} location", townLocation,
                    town.PlayerOwned ? "Target" : "Important"));
            }

            Mood = SceneSharing.enemyMood;
            Productivity = SceneSharing.enemyProductivity;
            if (SceneSharing.enemyControlledLocations > ImportantLocations.Count)
            {
                ProductivityDirection += .1f;
                Productivity -= 0.5f;
                Mood += 1f;
            }
            else
            {
                ProductivityDirection -= .8f;
                Productivity += 0.2f;
                Mood -= 0.5f;
            }
            turnManager.Subscribe(this);
        }

        private void Update()
        {
            if (Mood > Mathf.Max(1 - 0.02f * turnManager.TimeStep, 0.4f))
            {
                if (Odds.CoinToss())
                {
                    PlaceEnemy();
                    Mood = 0f;
                }
            }
            if (updateBehaviour)
            {
                foreach (EnemyMovementController enemy in Enemies)
                {
                    if (enemy.Location == null)
                    {
                        Debug.LogWarning("Delaying behaviour update till enemies are ready");
                        return;
                    }
                }
                if (mood < -0.3f)
                {
                    UpdateBehaviour(Behaviour.Defensive);
                }
                else if (mood > 0.3f)
                {
                    UpdateBehaviour(Behaviour.Aggresive);
                }
                else
                {
                    UpdateBehaviour(Behaviour.Passive);
                }
                updateBehaviour = false;
            }
        }

        private void OnDestroy()
        {
            SceneSharing.enemyControlledLocations = ImportantLocations.Count;
            SceneSharing.enemyMood = Mood;
            SceneSharing.enemyProductivity = Productivity;
            SceneSharing.enemyProductivityDirection = ProductivityDirection;
            foreach (EnemyMovementController enemy in Enemies)
            {
                enemy.Squad.X = enemy.Location.Position.GridPoint.X;
                enemy.Squad.Y = enemy.Location.Position.GridPoint.Y;
                enemy.Squad.Save();
            }
        }

        private EnemyMovementController PlaceEnemy(IHexGridCell location = null, IEnemy[] enemies = null)
        {
            if (location == null)
            {
                do
                {
                    location = Odds.SelectAtRandom(Odds.SelectAtRandom(ImportantLocations).Neighbors);
                } while (location.HasComponent<UnselectableComponent>());
            }
            if (enemies == null)
            {
                List<IEnemy> enemyList = new List<IEnemy>();
                int maxEnemyCount = (int)(2 + Mathf.Min(0.05f * turnManager.TimeStep, 3));
                int enemyCount = Odds.DiceRoll(maxEnemyCount) + 2;
                for (int i = 0; i < enemyCount; i++)
                {
                    enemyList.Add(Enemy.GetEnemyFromTier(1));
                }
                enemies = enemyList.ToArray();
            }

            EnemyMovementController enemy = Instantiate(enemyPrefab, transform);
            enemy.transform.position = location.Position.CenterAsVector3(enemyPlacementHeight);
            enemy.Enemies = enemies;
            enemy.StartLocation = location;
            turnManager.Subscribe(enemy);
            Enemies.Add(enemy);
            enemy.MovementStrategy = new FixedLocationMovementStrategy(location);
            enemy.Squad = new EnemySquad(location.Position.GridPoint.X, location.Position.GridPoint.Y);
            return enemy;
        }

        private void PlaceEnemy(IEnemySquad enemy)
        {
            EnemyMovementController movementController = PlaceEnemy(tileManager.HexTiling[enemy.X, enemy.Y]);
            movementController.Squad = enemy;
        }

        private void DetermineMovementStrategy(EnemyMovementController enemy)
        {
            KeyValuePair<SquadController, int> nearestSquad = GetNearest(squadManager.squads, enemy.Location, false);

            if (nearestSquad.Value <= guardDistance[currentBehaviour])
            {
                enemy.MovementStrategy = new GuardMovementStrategy(nearestSquad.Key.Location, guardDistance[currentBehaviour]);
                return;
            }
            if (chaserCount < Math.Min((int)(Enemies.Count / 3), squadManager.squads.Count))
            {
                chaserCount++;
                enemy.MovementStrategy = new ChaseMovementStrategy(nearestSquad.Key);
                return;
            }
            enemy.MovementStrategy = new WanderMovementStrategy();
        }

        private void UpdateBehaviour(Behaviour behaviour)
        {
            if (behaviour == currentBehaviour)
            {
                return;
            }
            chaserCount = 0;
            currentBehaviour = behaviour;
            foreach (EnemyMovementController enemy in Enemies)
            {
                enemy.MovementStrategy = null;
            }

            int locationsToProtect = Mathf.RoundToInt(ImportantLocations.Count * protectedLocationsPercents[behaviour]);
            for (int i = 0; i < locationsToProtect; i++)
            {
                IHexGridCell cellToGuard = ImportantLocations[i];
                while (cellToGuard.HasComponent<UnselectableComponent>())
                {
                    cellToGuard = Odds.SelectAtRandom(cellToGuard.Neighbors);
                }
                GetNearestUntasked(Enemies, cellToGuard).Key.MovementStrategy = new GuardMovementStrategy(cellToGuard, guardDistance[currentBehaviour]);
            }

            int locationsToAttack = Mathf.RoundToInt(TargetLocations.Count * (1 - protectedLocationsPercents[behaviour]));
            for (int i = 0; i < locationsToAttack; i++)
            {
                IHexGridCell cellToAttack = Odds.SelectAtRandom(TargetLocations);
                while (cellToAttack.HasComponent<UnselectableComponent>())
                {
                    cellToAttack = Odds.SelectAtRandom(cellToAttack.Neighbors);
                }
                GetNearestUntasked(Enemies, cellToAttack).Key.MovementStrategy = new FixedLocationMovementStrategy(cellToAttack);
            }

            foreach (EnemyMovementController enemy in Enemies)
            {
                if (enemy.MovementStrategy == null)
                {
                    DetermineMovementStrategy(enemy);
                }
            }
            Debug.Log(string.Format(
                "Enemy Behaviour Updated\n\tProtecting {0} of {1} important locations\n\tAttacking {2} of {3} target locations\n\tChasers active {4}",
                locationsToProtect, ImportantLocations.Count, locationsToAttack, TargetLocations.Count, chaserCount));
        }

        private static KeyValuePair<T, int> GetNearest<T>(IEnumerable<T> creatures, IHexGridCell destination, bool isEnemyList = true) where T : CreatureMovementController
        {
            KeyValuePair<T, int> nearestCreature = new KeyValuePair<T, int>(null, int.MaxValue);
            foreach (T creature in creatures)
            {
                int distanceToSquad = AStarModule.FindPath(creature.Location, destination, !isEnemyList).Count;
                if (distanceToSquad < nearestCreature.Value)
                {
                    nearestCreature = new KeyValuePair<T, int>(creature, distanceToSquad);
                }
            }
            return nearestCreature;
        }

        private static KeyValuePair<EnemyMovementController, int> GetNearestUntasked(IList<EnemyMovementController> creatures, IHexGridCell destination)
        {
            IList<EnemyMovementController> localEnemies = new List<EnemyMovementController>(creatures);
            KeyValuePair<EnemyMovementController, int> nearestCreature = GetNearest(localEnemies, destination);
            while (nearestCreature.Key.MovementStrategy != null && localEnemies.Count > 0)
            {
                localEnemies.Remove(nearestCreature.Key);
                nearestCreature = GetNearest(localEnemies, destination);
            }
            return nearestCreature;
        }

#if (UNITY_EDITOR)
        [CustomEditor(typeof(EnemyBrain))]
        public class EditorButton : Editor
        {
            public override void OnInspectorGUI() //2
            {
                base.DrawDefaultInspector();
                EnemyBrain manage = (EnemyBrain)target; //1

                GUILayout.Space(20f); //2
                GUILayout.Label("Custom Editor Elements", EditorStyles.boldLabel); //3
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Manually Update Behaviour")) //8
                {
                    manage.updateBehaviour = true;
                }

                GUILayout.EndHorizontal();
            }
        }
#endif
    }
}
