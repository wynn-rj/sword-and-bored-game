using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Utilities.Random;

namespace SwordAndBored.Strategy.Movement
{
    public class EnemyMovementController : CreatureMovementController
    {
        public IEnemy[] Enemies { get; set; }

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

        private void DeterminePath()
        {
            IHexGridCell gridCell = Location;
            for (int i = 0; i < averageMovement; i++)
            {
                do
                {
                    gridCell = Odds.SelectAtRandom(gridCell.Neighbors);
                } while (gridCell is null);
                path.Enqueue(gridCell);
            }
        }
    }
}
