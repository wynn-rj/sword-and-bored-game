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
using UnityEngine;

namespace SwordAndBored.Strategy.EnemyManagement
{
    class EnemyBrain : MonoBehaviour, IPreTimeStepSubscriber
    {
        [System.Serializable]
        private enum Behaviour
        {
            Aggresive,
            Passive,
            Defensive,
            Loading
        }

        private const int GUARD_DISTANCE_MODIFIER = 2;
        private const float PRODUCTIVITY_DIRECTION_BOUND = 0.2f;

        private readonly Dictionary<Behaviour, int> guardDistance = new Dictionary<Behaviour, int>()
        {
            { Behaviour.Aggresive, -GUARD_DISTANCE_MODIFIER },
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
            }
        }

        private float Productivity
        {
            get => productivity;
            set => Productivity = Mathf.Clamp(value, -1f, 1f);
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
        [SerializeField] private float mood;
        [SerializeField] private float productivity;
        [SerializeField] private float productivityDirection = 0;
        private int chaserCount = 0;

        public void PreTimeStepUpdate()
        {
            Mood += 0.05f;
            int desiredEnemyCount = ImportantLocations.Count + TargetLocations.Count + squadManager.squads.Count + Mathf.RoundToInt(0.08f * turnManager.TimeStep);
            ProductivityDirection += 0.001f * (desiredEnemyCount - Enemies.Count);
            productivity += ProductivityDirection;

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
            foreach (IHexGridCell neighbor in tileManager.EnemyBase.Neighbors)
            {
                PlaceEnemy(neighbor);
            }
            foreach (ITown town in Town.GetAllTowns())
            {
                if (town.PlayerOwned)
                {
                    TargetLocations.Add(tileManager.HexTiling[town.X, town.Y]);
                }
                else
                {
                    ImportantLocations.Add(tileManager.HexTiling[town.X, town.Y]);
                }
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
        }

        private void OnDestroy()
        {
            SceneSharing.enemyControlledLocations = ImportantLocations.Count;
            SceneSharing.enemyMood = Mood;
            SceneSharing.enemyProductivity = Productivity;
            SceneSharing.enemyProductivityDirection = ProductivityDirection;
        }

        private void PlaceEnemy(IHexGridCell location = null, IEnemy[] enemies = null)
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
            }
           
            EnemyMovementController enemy = Instantiate(enemyPrefab, transform);
            enemy.transform.position = location.Position.CenterAsVector3(enemyPlacementHeight);
            enemy.Enemies = enemies;
            enemy.StartLocation = location;
            turnManager.Subscribe(enemy);
            Enemies.Add(enemy);
            enemy.MovementStrategy = new FixedLocationMovementStrategy(location);
        }

        private void DetermineMovementStrategy(EnemyMovementController enemy)
        {
            KeyValuePair<SquadController, int> nearestSquad = GetNearest(squadManager.squads, enemy.Location);

            if (nearestSquad.Value <= guardDistance[currentBehaviour] + GUARD_DISTANCE_MODIFIER)
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
                GetNearest(Enemies, cellToGuard).Key.MovementStrategy = new GuardMovementStrategy(cellToGuard, guardDistance[currentBehaviour]);
            }

            int locationsToAttack = Mathf.RoundToInt(TargetLocations.Count * (1 - protectedLocationsPercents[behaviour]));            
            for (int i = 0; i < locationsToAttack; i++)
            {
                IHexGridCell cellToAttack = Odds.SelectAtRandom(TargetLocations);
                while (cellToAttack.HasComponent<UnselectableComponent>())
                {
                    cellToAttack = Odds.SelectAtRandom(cellToAttack.Neighbors);
                }
                GetNearest(Enemies, cellToAttack).Key.MovementStrategy = new FixedLocationMovementStrategy(cellToAttack);
            }

            foreach (EnemyMovementController enemy in Enemies)
            {
                if (enemy.MovementStrategy == null)
                {
                    DetermineMovementStrategy(enemy);
                }
            }
        }

        private static KeyValuePair<T, int> GetNearest<T>(IEnumerable<T> creatures, IHexGridCell destination) where T : CreatureMovementController
        {
            KeyValuePair<T, int> nearestCreature = new KeyValuePair<T, int>(null, int.MaxValue);
            foreach (T creature in creatures)
            {
                int distanceToSquad = AStarModule.FindPath(creature.Location, destination).Count;
                if (distanceToSquad < nearestCreature.Value)
                {
                    nearestCreature = new KeyValuePair<T, int>(creature, distanceToSquad);
                }
            }
            return nearestCreature;
        }
    }
}
