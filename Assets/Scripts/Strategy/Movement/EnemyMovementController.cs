using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.Movement.EnemyMovementStrategies;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.Transitions;
using SwordAndBored.Utilities.Debug;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.Movement
{
    public class EnemyMovementController : CreatureMovementController
    {
        public IEnemy[] Enemies { get; set; }
        public IEnemyMovementStrategy MovementStrategy
        {
            get => movementStrategy;
            set
            {
                movementStrategy = value;
                name = string.Format("Enemy ({0})", movementStrategy?.GetType().Name.Replace("MovementStrategy", ""));
                debugName = name + " @ " + Location?.Position;
            }
        }
        public IEnemySquad Squad { get; set; }

        [SerializeField] private int manualSetGoalX;
        [SerializeField] private int manualSetGoalY;
        [SerializeField] private bool useManualGoal = false;
#if DEBUG
        [SerializeField] private string movementDetails = "";
        private bool updateMovementDescription = true;
#endif

        private IEnemyMovementStrategy movementStrategy;
        private int averageMovement;

        public override void PostTimeStepUpdate()
        {
            DeterminePath();
            base.PostTimeStepUpdate();
        }

        protected override int ResetMovement()
        {
            return averageMovement;
        }

        protected override void Start()
        {            
            AssertHelper.Assert(Enemies != null && Enemies.Length > 0, "Enemy controller has no enemies", this);
            averageMovement = 0;
            foreach (IEnemy enemy in Enemies)
            {
                averageMovement += enemy.Stats.Movement;
            }
            averageMovement /= Enemies.Length;
            base.Start();
        }

#if DEBUG
        protected override void Update()
        {
            base.Update();
            if (useManualGoal)
            {
                movementDetails = "Following manual goal";
            }
            else
            {
                movementDetails = movementStrategy?.ToString();
            }
        }
#endif

        protected override void ArrivedAtNewLocation()
        {
            base.ArrivedAtNewLocation();

            CreepComponent creepComponent = Location.GetComponent<CreepComponent>();
            if (!(creepComponent is null))
            {
                creepComponent.IsCreepActive = true;
            }

            if (Location.HasComponent<TownComponent>() && !Location.HasComponentThatIsNot(creatureComponent))
            {
                ITown town = Location.GetComponent<TownComponent>().Town;
                List<IUnit> units = town.Units;
                if (units?.Count > 0)
                {
                    Squad defenseSquad = new Squad("Town defenders", units, town.X, town.Y);
                    defenseSquad.Save();
                    BattleStarter.StartBattle(Location, defenseSquad);
                }
                else
                {
                    town.PlayerOwned = false;
                    town.Save();
                }
            }
        }

        protected override bool OccupiableLocation(IHexGridCell location)
        {
            CreatureComponent tileCreatureComponent = location.GetComponent<CreatureComponent>();
            return tileCreatureComponent == null || tileCreatureComponent.Creature.GetType() != typeof(EnemyMovementController);
        }

        private void DeterminePath()
        {
            AssertHelper.Assert(Location != null, "Location was null!", this);
            AssertHelper.Assert(MovementStrategy != null, "Enemy doesn't know how to move!", this);
            path.Clear();
            List<IHexGridCell> pathToGoal = !useManualGoal ? MovementStrategy.GetPath(Location, averageMovement) :
                 AStarModule.FindPath(Location, Location.ParentGrid[manualSetGoalX, manualSetGoalY], false);
            foreach (IHexGridCell cell in pathToGoal)
            {
                path.Enqueue(cell);
            }
#if DEBUG
            updateMovementDescription = true;
#endif
        }
    }
}
