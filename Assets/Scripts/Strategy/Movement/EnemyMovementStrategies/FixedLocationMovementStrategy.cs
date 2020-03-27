﻿using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    public class FixedLocationMovementStrategy : IEnemyMovementStrategy
    {
        protected IHexGridCell fixedLocation;

        public FixedLocationMovementStrategy(IHexGridCell location)
        {
            fixedLocation = location;
        }

        public virtual List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed)
        {
            if (currentLocation != fixedLocation)
            {
                return AStarModule.FindPath(currentLocation, fixedLocation);
            }
            return new List<IHexGridCell>();
        }
    }
}