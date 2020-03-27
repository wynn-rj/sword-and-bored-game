using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using System;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    class GuardMovementStrategy : FixedLocationMovementStrategy
    {
        private readonly List<IHexGridCell> guardedCells;

        public GuardMovementStrategy(IHexGridCell locationToGuard, int guardRadius) : base(locationToGuard)
        {
            guardedCells = new List<IHexGridCell>();
            AddGuardedCells(locationToGuard, guardRadius);
        }

        public override List<IHexGridCell> GetPath(IHexGridCell currentLocation, int speed)
        {
            foreach (IHexGridCell cell in guardedCells)
            {
                CreatureComponent creatureComponent = cell.GetComponent<CreatureComponent>();
                if (creatureComponent?.Creature?.GetType() == typeof(CreatureMovementController))
                {
                    return AStarModule.FindPath(currentLocation, creatureComponent.Creature.Location);
                }
            }
            return base.GetPath(currentLocation, speed);
        }

        private void AddGuardedCells(IHexGridCell baseCell, int radius)
        {
            if (radius == 0)
            {
                if (!guardedCells.Contains(baseCell))
                {
                    guardedCells.Add(baseCell);
                }
                return;
            }
            foreach (IHexGridCell cell in baseCell.Neighbors)
            {
                AddGuardedCells(cell, radius - 1);
            }
        }
    }
}
