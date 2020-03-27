using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.Random;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    class WanderMovementStrategy : IEnemyMovementStrategy
    {
        private const int MAX_ITERATIONS = 20;

        public List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed)
        {
            IHexGridCell newLocation;
            int iterations = 0;
            do {
                Point<int> start = currentLocation.Position.GridPoint;
                Point<int> randomOffset = new Point<int>(start.X - speed + Odds.DiceRoll(2 * speed), start.Y - speed + Odds.DiceRoll(2 * speed));
                newLocation = currentLocation.ParentGrid[randomOffset];
                iterations++;
            } while ((newLocation == null || newLocation.HasComponent<UnselectableComponent>()) && iterations < MAX_ITERATIONS);
            if (iterations == MAX_ITERATIONS)
            {
                return new List<IHexGridCell>();
            }
            return AStarModule.FindPath(currentLocation, newLocation, false);
        }

        public override string ToString()
        {
            return "Wandering";
        }
    }
}
