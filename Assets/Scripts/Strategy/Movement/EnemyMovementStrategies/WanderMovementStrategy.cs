using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    class WanderMovementStrategy : IEnemyMovementStrategy
    {
        private const int MAX_ITERATIONS = 10;

        public List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed)
        {
            IHexGridCell newLocation;
            int iterations = 0;
            do {
                Point<int> start = currentLocation.Position.GridPoint;
                Point<int> randomOffset = new Point<int>(start.X - speed + Odds.DiceRoll(2 * speed), start.Y - speed + Odds.DiceRoll(2 * speed));
                newLocation = currentLocation.ParentGrid[randomOffset];
            } while (!newLocation.HasComponent<UnselectableComponent>() && iterations++ < MAX_ITERATIONS);
            if (iterations == MAX_ITERATIONS)
            {
                return new List<IHexGridCell>();
            }
            return AStarModule.FindPath(currentLocation, newLocation);
        }
    }
}
