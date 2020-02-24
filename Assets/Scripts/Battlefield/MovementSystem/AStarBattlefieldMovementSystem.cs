using System.Net;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace SwordAndBored.Battlefield.MovementSystem
{
    public class AStarBattlefieldMovementSystem : MonoBehaviour
    {

        private GridHolder holder;

        public AStarBattlefieldMovementSystem(GridHolder holder)
        {
            this.holder = holder;
        }

        public Stack<Tile> findPath(Vector2 startPoint, Vector2 destination)
        {
            return findPath(new Vector2Int((int)startPoint.x, (int)startPoint.y), new Vector2Int((int)destination.x, (int)destination.y));
        }

        public Stack<Tile> findPath(Vector2Int startPoint, Vector2Int destination)
        {
            if (holder.tiles[startPoint.x, startPoint.y] == null)
            {
                Debug.Log("Start Point is null");
            }
            Debug.Log("start:" + startPoint);
            Debug.Log("destination: " + destination);
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            Node destinationNode = new Node(destination, null);

            openList.Add(new Node(startPoint, null));

            while (openList.Count > 0)
            {
                float f = openList[0].F;
                int idx = 0;
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].F < f)
                    {
                        f = openList[i].F;
                        idx = i;
                    }
                }
                Node current = openList[idx];
                openList.RemoveAt(idx);
                List<Node> successors = generateSuccesors(current);
                for (int i = 0; i < successors.Count; i++)
                {
                    bool shouldAdd = true;
                    Node successor = successors[i];

                    if (successor.isLocationSame(destinationNode))
                    {
                        Stack<Tile> returnPathStack = generatePathStack(successor);
                        return returnPathStack;
                    }

                    successor.H = (destination.x - successor.Location.x) + (destination.y - successor.Location.y);

                    foreach (Node currentOpen in openList)
                    {
                        if (successor.isLocationSame(currentOpen) && (successor.F >= currentOpen.F))
                        {
                            shouldAdd = false;
                            break;
                        }
                    }

                    if (shouldAdd)
                    {
                        foreach (Node currentClosed in closedList)
                        {
                            if (successor.isLocationSame(currentClosed) && (successor.F >= currentClosed.F))
                            {
                                shouldAdd = false;
                            }
                        }
                    }

                    if (shouldAdd) openList.Add(successor);
                }
                closedList.Add(current);
            }


            return new Stack<Tile>();
        }

        private Stack<Tile> generatePathStack(Node finalNode)
        {
            Stack<Tile> pathStack = new Stack<Tile>();
            Node current = finalNode;
            pathStack.Push(GetTileFromNode(current));
            while (current.Parent != null)
            {
                pathStack.Push(GetTileFromNode(current.Parent));
                current = current.Parent;
            }
            return pathStack;
        }

        private List<Node> generateSuccesors(Node parent)
        {
            List<Node> succesors = new List<Node>();

            int x = parent.Location.x;
            int y = parent.Location.y;

            if (x + 1 < holder.tiles.GetLength(0)) succesors.Add(new Node(new Vector2Int(x + 1, y), parent, 1));
            if (y + 1 < holder.tiles.GetLength(1)) succesors.Add(new Node(new Vector2Int(x, y + 1), parent, 1));
            if (x - 1 >= 0) succesors.Add(new Node(new Vector2Int(x - 1, y), parent, 1));
            if (y - 1 >= 0) succesors.Add(new Node(new Vector2Int(x, y - 1), parent, 1));

            return succesors;

        }

        private Tile GetTileFromNode(Node node)
        {
            return holder.tiles[node.Location.x, node.Location.y];
        }

        private class Node
        {
            public Vector2Int Location { get; }
            public Node Parent { get; }
            public float G { get; set; }
            public float H { get; set; }
            public float F { get { return G + H; } }


            public Node(Vector2Int location, Node parent, int g = 0, int h = 0)
            {
                Location = location;
                Parent = parent;
                G = g;
                H = h;
            }

            public bool isLocationSame(Node other)
            {
                return this.Location.x == other.Location.x && this.Location.y == other.Location.y;
            }
        }
    }

}