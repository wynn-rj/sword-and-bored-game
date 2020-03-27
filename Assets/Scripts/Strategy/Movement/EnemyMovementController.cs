using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.Movement.EnemyMovementStrategies;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.Debug;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.Movement
{
    public class EnemyMovementController : CreatureMovementController
    {
        public IEnemy[] Enemies { get; set; }
        public IEnemyMovementStrategy MovementStrategy { get; set; }

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
            AssertHelper.Assert(Location != null, "Location was null!", this);
            AssertHelper.Assert(MovementStrategy != null, "Enemy doesn't know how to move!", this);
            path.Clear();
            List<IHexGridCell> pathToGoal = !useManualGoal ? MovementStrategy.GetPath(Location, averageMovement) :
                 AStarModule.FindPath(Location, Location.ParentGrid[manualSetGoalX, manualSetGoalY]);
            foreach (IHexGridCell cell in pathToGoal)
            {
                path.Enqueue(cell);
            }
        }
    }
}
