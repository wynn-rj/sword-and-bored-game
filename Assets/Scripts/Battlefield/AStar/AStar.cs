using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;


namespace SwordAndBored.Battlefield.AStar
{
    class Node
    {
        public int x;
        public int y;

        public int f;
        public int h;
        public int g;

        public Node parent;

        public Node(Node parent, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;
        }

    }


    public class AStar
    {

        public List<Tile> FindPath(Tile goal, GridHolder grid, UniqueCreature creature)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            Node start = new Node(null, creature.currentTile.x, creature.currentTile.y);
            start.g = start.h = start.f = 0;
            Node end = new Node(null, goal.x, goal.y);
            end.g = end.h = end.f = 0;

            openList.Add(start);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
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
                    Node current = currentNode;
                    while (current != null)
                    {
                        path.Add(grid.tiles[current.x, current.y]);
                        current = current.parent;
                    }
                    return path;
                }

                List<Node> children = new List<Node>();

                //check surronding tiles
                Node nodePos = new Node(currentNode, currentNode.x + 1, currentNode.y);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid.tiles[nodePos.x, nodePos.y] && grid.tiles[nodePos.x, nodePos.y].walkable && !grid.tiles[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }
                nodePos = new Node(currentNode, currentNode.x, currentNode.y + 1);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid.tiles[nodePos.x, nodePos.y] && grid.tiles[nodePos.x, nodePos.y].walkable && !grid.tiles[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }
                nodePos = new Node(currentNode, currentNode.x - 1, currentNode.y);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid.tiles[nodePos.x, nodePos.y] && grid.tiles[nodePos.x, nodePos.y].walkable && !grid.tiles[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }
                nodePos = new Node(currentNode, currentNode.x, currentNode.y - 1);
                if (nodePos.x < 49 && nodePos.y < 49)
                {
                    if (grid.tiles[nodePos.x, nodePos.y] && grid.tiles[nodePos.x, nodePos.y].walkable && !grid.tiles[nodePos.x, nodePos.y].unitOnTile)
                    {
                        children.Add(nodePos);
                    }
                }

                foreach (Node child in children)
                {
                    bool stop = false;
                    foreach (Node closedChild in closedList)
                    {
                        if (!stop && child == closedChild)
                        {
                            stop = true;
                        }
                    }
                    if (!stop)
                    {
                        child.g = currentNode.g + 1;
                        child.h = Mathf.RoundToInt(Vector3.Distance(goal.transform.position, grid.tiles[child.x, child.y].transform.position));
                        child.f = child.g + child.h;

                        foreach (Node openNode in openList)
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
