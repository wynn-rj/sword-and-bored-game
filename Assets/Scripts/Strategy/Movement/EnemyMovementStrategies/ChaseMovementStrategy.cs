using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    class ChaseMovementStrategy : IEnemyMovementStrategy
    {
        private readonly CreatureMovementController chasing;
        private readonly string chasingName;

        public ChaseMovementStrategy(CreatureMovementController creatureToChase)
        {
            chasing = creatureToChase;
            chasingName = chasing.name;
        }

        public List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed)
        {
            return AStarModule.FindPath(currentLocation, chasing.Location, false);
        }

        public override string ToString()
        {
            return "Chasing " + chasingName;
        }
    }
}
