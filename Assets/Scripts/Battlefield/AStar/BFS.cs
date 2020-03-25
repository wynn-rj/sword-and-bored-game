using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.MovementSystemScripts;

namespace SwordAndBored.Battlefield.AstarStuff
{
    class BFSNode
    {
        public int x;
        public int y;

        public int f;
        public int h;
        public int g;

        public BFSNode parent;

        public BFSNode(BFSNode parent, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;
        }

    }


    public class BFS
    {

        public List<Tile> FindMoveZone(Tile goal, Tile[,] grid, MovementSystem ms)
        {
            List<BFSNode> openList = new List<BFSNode>();
            List<BFSNode> closedList = new List<BFSNode>();

            BFSNode start = new BFSNode(null, ms.currentTile.x, ms.currentTile.y);
            start.g = start.h = start.f = 0;
            BFSNode end = new BFSNode(null, goal.x, goal.y);
            end.g = end.h = end.f = 0;

            openList.Add(start);

            while (openList.Count > 0)
            {
                BFSNode currentNode = openList[0];
                int currentIndex = 0;
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].f < currentNode.f)
                    {
                        currentNode = openList[i];
                        currentIndex = i;
                    }
                }

                openList.RemoveAt(currentIndex);
                closedList.Add(currentNode);

                if (currentNode.x == end.x && currentNode.y == end.y)
                {
                    List<Tile> path = new List<Tile>();
                    BFSNode current = currentNode;
                    while (current != null)
                    {
                        path.Add(grid[current.x, current.y]);
                        current = current.parent;
                    }
                    return path;
                }

                List<BFSNode> children = new List<BFSNode>();

                //check surronding tiles
                BFSNode nodePos = new BFSNode(currentNode, currentNode.x + 1, currentNode.y);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }
                nodePos = new BFSNode(currentNode, currentNode.x, currentNode.y + 1);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }
                nodePos = new BFSNode(currentNode, currentNode.x - 1, currentNode.y);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }
                nodePos = new BFSNode(currentNode, currentNode.x, currentNode.y - 1);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }

                foreach (BFSNode child in children)
                {
                    bool stop = false;
                    foreach (BFSNode closedChild in closedList)
                    {
                        if (!stop && child == closedChild)
                        {
                            stop = true;
                        }
                    }
                    if (!stop)
                    {
                        child.g = currentNode.g + 1;
                        child.h = Mathf.RoundToInt(Vector3.Distance(goal.transform.position, grid[child.x, child.y].transform.position));
                        child.f = child.g + child.h;

                        foreach (BFSNode openNode in openList)
                        {
                            if (!stop && child == openNode && child.g > openNode.g)
                            {
                                stop = true;
                            }
                        }
                        if (!stop)
                        {
                            openList.Add(child);
                        }
                    }
                }
            }
        
            return null;
        }
    }
}
