using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    class ChaseMovementStrategy : IEnemyMovementStrategy
    {
        private readonly CreatureMovementController chassee;

        public ChaseMovementStrategy(CreatureMovementController creatureToChase)
        {
            this.chassee = creatureToChase;
        }

        public List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed)
        {
            return AStarModule.FindPath(currentLocation, chassee.Location);
        }
    }
}
