using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    public interface IEnemyMovementStrategy
    {
        List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed);
    }
}
