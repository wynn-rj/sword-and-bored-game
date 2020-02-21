using UnityEngine;
using SwordAndBored.Utilities.AStarAlgorithm;
using System.Collections.Generic;
using System;

namespace SwordAndBored.Battlefield.MovementSystem
{
    public class AStarBattlefieldMovementSystem
    {
        private AStarBattlefieldModule astar;

        public AStarBattlefieldMovementSystem(Tile[,] tiles)
        {
            astar = new AStarBattlefieldModule(tiles);
        }

        public void HighlightPath(Tile currentTile, Tile destinationTile)
        {
            Tuple<int, int> startPoint = Tuple.Create((int)currentTile.CoordinatesOnGrid.x,
                                                (int)currentTile.CoordinatesOnGrid.y);
            Tuple<int, int> destination = Tuple.Create((int)destinationTile.CoordinatesOnGrid.x,
                                                (int)destinationTile.CoordinatesOnGrid.y);

            Stack<Tile> pathStack = astar.findPath(startPoint, destination);

            while (pathStack.Count > 0)
            {
                Tile current = pathStack.Pop();
                current.GetComponent<Renderer>().material.color = Color.blue;
            }

        }
    }

}