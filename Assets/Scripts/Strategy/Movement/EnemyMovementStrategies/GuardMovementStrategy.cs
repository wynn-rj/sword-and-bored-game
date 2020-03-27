using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using System;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement.EnemyMovementStrategies
{
    class GuardMovementStrategy : FixedLocationMovementStrategy
    {
        private readonly List<IHexGridCell> guardedCells;
        private CreatureComponent chasing;

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
                    chasing = creatureComponent;
                    return AStarModule.FindPath(currentLocation, creatureComponent.Creature.Location, false);
                }
            }
            chasing = null;
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

        public override string ToString()
        {
            if (chasing == null)
            {
                return "Guarding " + fixedLocation.Position.ToString();
            }
            return "Guarding " + fixedLocation.Position.ToString() + " and chasing " + chasing.Creature.name;
        }
    }
}
