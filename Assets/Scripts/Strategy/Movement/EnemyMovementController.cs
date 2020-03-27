using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Utilities.Random;
using UnityEngine;

namespace SwordAndBored.Strategy.Movement
{
    public class EnemyMovementController : CreatureMovementController
    {
        public IEnemy[] Enemies { get; set; }

        [SerializeField] private int manualSetGoalX;
        [SerializeField] private int manualSetGoalY;
        [SerializeField] private bool useManualGoal = false;

        private int averageMovement;

        public override void PreTimeStepUpdate()
        {
            base.PreTimeStepUpdate();
            DeterminePath();
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
            DeterminePath();
        }

        protected override void ArrivedAtNewLocation()
        {
            base.ArrivedAtNewLocation();

            CreepComponent creepComponent = Location.GetComponent<CreepComponent>();
            if (!(creepComponent is null))
            {
                creepComponent.IsCreepActive = true;
            }
        }

        protected override bool OccupiableLocation(IHexGridCell location)
        {
            creatureComponent = location.GetComponent<CreatureComponent>();
            return creatureComponent?.Creature?.GetType() == typeof(EnemyMovementController);
        }

        private void DeterminePath()
        {
            path.Clear();
            if (useManualGoal)
            {
                foreach (IHexGridCell cell in AStarModule.FindPath(Location, Location.ParentGrid[manualSetGoalX, manualSetGoalY]))
                {
                    path.Enqueue(cell);
                }
                return;
            }
            AssertHelper.Assert(Location != null, "Location was null!", this);
            IHexGridCell gridCell = Location;
            for (int i = 0; i < averageMovement; i++)
            {
                IHexGridCell tempGridCell;
                do
                {
                    tempGridCell = Odds.SelectAtRandom(gridCell.Neighbors);
                } while (tempGridCell is null || tempGridCell.HasComponent<UnselectableComponent>());
                gridCell = tempGridCell;
                path.Enqueue(gridCell);
            }
        }
    }
}
