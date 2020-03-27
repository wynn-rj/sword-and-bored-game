using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    public interface IEnemyMovementStrategy
    {
        public List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed);
    }
}
