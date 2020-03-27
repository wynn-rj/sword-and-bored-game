using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.MovementSystemScripts;

namespace SwordAndBored.Battlefield.AstarStuff
{
    class NodeB
    {
        public int x;
        public int y;

        public int distance;

        public bool visted = false;

        public NodeB(int x, int y, int distance)
        {
            this.x = x;
            this.y = y;
            this.distance = distance;
        }

    }

    public class BFS
    {
        public List<Tile> GetPossibleMove(Tile[,] grid, MovementSystem ms, int movement)
        {
            int movementLeft = movement + 1;
            Queue<NodeB> Q = new Queue<NodeB>();
            List<NodeB> result = new List<NodeB>();
            NodeB s = new NodeB(ms.currentTile.x, ms.currentTile.y, movementLeft);
            Q.Enqueue(s);
            while (Q.Count > 0)
            {
                NodeB v = Q.Dequeue();


                NodeB check = new NodeB(v.x, v.y + 1, v.distance - 1);
                if (grid[check.x, check.y] && grid[check.x, check.y].walkable && !grid[check.x, check.y].unitOnTile)
                {
                    if (Mathf.Abs(check.x) < 49 && Mathf.Abs(check.y) < 49 && Mathf.Abs(check.x) > 0 && Mathf.Abs(check.y) > 0)
                    {
                        if (!check.visted)
                        {
                            check.visted = true;
                            if (check.distance > 0)
                            {
                                Q.Enqueue(check);
                                result.Add(check);
                            }
                        }
                    }
                }
                check = new NodeB(v.x, v.y - 1, v.distance - 1);
                if (grid[check.x, check.y] && grid[check.x, check.y].walkable && !grid[check.x, check.y].unitOnTile)
                {
                    if (Mathf.Abs(check.x) < 49 && Mathf.Abs(check.y) < 49 && Mathf.Abs(check.x) > 0 && Mathf.Abs(check.y) > 0)
                    {
                        if (!check.visted)
                        {
                            check.visted = true;
                            if (check.distance > 0)
                            {
                                Q.Enqueue(check);
                                result.Add(check);
                            }
                        }
                    }
                }
                check = new NodeB(v.x - 1, v.y, v.distance - 1);
                if (grid[check.x, check.y] && grid[check.x, check.y].walkable && !grid[check.x, check.y].unitOnTile)
                {
                    if (Mathf.Abs(check.x) < 49 && Mathf.Abs(check.y) < 49 && Mathf.Abs(check.x) > 0 && Mathf.Abs(check.y) > 0)
                    {
                        if (!check.visted)
                        {
                            check.visted = true;
                            if (check.distance > 0)
                            {
                                Q.Enqueue(check);
                                result.Add(check);
                            }
                        }
                    }
                }
                check = new NodeB(v.x + 1, v.y, v.distance - 1);
                if (grid[check.x, check.y] && grid[check.x, check.y].walkable && !grid[check.x, check.y].unitOnTile)
                {
                    if (Mathf.Abs(check.x) < 49 && Mathf.Abs(check.y) < 49 && Mathf.Abs(check.x) > 0 && Mathf.Abs(check.y) > 0)
                    {
                        if (!check.visted)
                        {
                            check.visted = true;
                            if (check.distance > 0)
                            {
                                Q.Enqueue(check);
                                result.Add(check);
                            }
                        }
                    }
                }
            }

            List<Tile> list = new List<Tile>();
            foreach (NodeB node in result)
            {
                list.Add(grid[node.x, node.y]);
            }
            return list;
        }
    }
}
