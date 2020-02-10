using UnityEngine;
using SwordAndBored.Utilities.AStarAlgorithm;
using System.Collections.Generic;
using System;

namespace SwordAndBored.Battlefield.MovementSystem
{
    public class AStarMovementSystem : MonoBehaviour
    {
        [SerializeField]
        private GridHolder tileContainer;

        [SerializeField]
        private AStarModule<GameObject> astar;

        public void Awake()
        {
            GridHolder tileContainer = GameObject.Find("TileContainer").GetComponent<GridHolder>();
            astar = new AStarModule<GameObject>(tileContainer.tiles);
        }

        public void HighlightPath(UnityEngine.AI.NavMeshAgent agent, GameObject currentTile, GameObject requestedTile)
        {
            Tuple<int, int> startPoint = findTileIndex(tileContainer.tiles, currentTile);
            Tuple<int, int> destination = findTileIndex(tileContainer.tiles, requestedTile);

            Stack<GameObject> pathStack = astar.findPath(startPoint, destination);

            while (pathStack.Count > 0)
            {
                GameObject current = pathStack.Pop();
                current.GetComponent<Renderer>().material.color = Color.blue;
            }

        }

        private Tuple<int, int> findTileIndex(GameObject[,] tiles, GameObject tile)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] == tile)
                        return Tuple.Create(i, j);
                }
            }

            return Tuple.Create(0, 0);
        }
    }

}