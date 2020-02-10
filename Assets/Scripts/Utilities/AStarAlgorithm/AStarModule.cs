using System.Collections.Generic;
using System;

namespace SwordAndBored.Utilities.AStarAlgorithm
{
    public class AStarModule<T>
    {
        private T[,] grid;

        public AStarModule(T[,] grid)
        {
            this.grid = grid;
        }

        public Stack<T> findPath(Tuple<int, int> startPoint, Tuple<int, int> destination)
        {
            Queue<Node> openList = new Queue<Node>();
            Queue<Node> closedList = new Queue<Node>();

            Node destinationNode = new Node(grid, destination, null);

            openList.Enqueue(new Node(grid, startPoint, null));

            while (openList.Count > 0)
            {
                Node current = openList.Dequeue();
                List<Node> successors = generateSuccesors(current);
                for (int i = 0; i < successors.Count; i++)
                {
                    bool shouldAdd = true;
                    Node successor = successors[i];

                    if (successor.isLocationSame(destinationNode))
                    {
                        Stack<T> returnPathStack = generatePathStack(successor);
                        return returnPathStack;
                    }

                    successor.H = (destination.Item1 - successor.Location.Item1) + (destination.Item2 - successor.Location.Item2);

                    foreach (Node currentOpen in openList)
                    {
                        if (successor.isLocationSame(currentOpen) && (successor.F > currentOpen.F))
                        {
                            shouldAdd = false;
                            break;
                        }
                    }

                    if (shouldAdd)
                    {
                        foreach (Node currentOpen in openList)
                        {
                            if (successor.isLocationSame(currentOpen) && (successor.F > currentOpen.F))
                            {
                                shouldAdd = false;
                            }
                        }
                    }

                    if (shouldAdd) openList.Enqueue(successor);
                }
                closedList.Enqueue(current);
            }


            return new Stack<T>();
        }

        private Stack<T> generatePathStack(Node finalNode)
        {
            Stack<T> pathStack = new Stack<T>();
            Node current = finalNode;
            pathStack.Push(current.Data);
            while (current.Parent != null)
            {
                pathStack.Push(current.Parent.Data);
                current = current.Parent;
            }

            return pathStack;

        }

        private List<Node> generateSuccesors(Node parent)
        {
            List<Node> succesors = new List<Node>();

            int x = parent.Location.Item1;
            int y = parent.Location.Item2;

            if (x + 1 < grid.GetLength(0)) succesors.Add(new Node(grid, Tuple.Create(x + 1, y), parent, 1));
            if (y + 1 < grid.GetLength(1)) succesors.Add(new Node(grid, Tuple.Create(x, y + 1), parent, 1));
            if (x - 1 >= 0) succesors.Add(new Node(grid, Tuple.Create(x - 1, y), parent, 1));
            if (y - 1 >= 0) succesors.Add(new Node(grid, Tuple.Create(x, y - 1), parent, 1));

            return succesors;

        }

        private class Node
        {
            public T[,] Grid { get; }
            public Tuple<int, int> Location { get; }
            public T Data { get { return Grid[Location.Item1, Location.Item2]; } }
            public Node Parent { get; }
            public float G { get; set; }
            public float H { get; set; }
            public float F { get { return G + H; } }


            public Node(T[,] grid, Tuple<int, int> location, Node parent, int g = 0, int h = 0)
            {
                Location = location;
                Parent = parent;
                Grid = grid;
                G = g;
                H = h;
            }

            public bool isLocationSame(Node other)
            {
                return this.Location.Item1 == other.Location.Item1 && this.Location.Item2 == other.Location.Item2;
            }
        }
    }
}
