using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Movement
{
    public class AStarModule
    {

        class Node
        {
            public IHexGridCell Cell { get; }
            public Node Parent { get; }
            public float G { get; set; }
            public float H { get; set; }

            public float F { get { return G + H; } }

            public Node(IHexGridCell cell, Node parent, float g = 0, float h = 0)
            {
                Cell = cell;
                Parent = parent;
                G = g;
                H = h;
            }

            public bool IsSameCell(Node other)
            {
                return Cell == other.Cell;
            }

        }

        public static List<IHexGridCell> FindPath(IHexGridCell startCell, IHexGridCell destinationCell)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            Node startNode = new Node(startCell, null);
            Node destinationNode = new Node(destinationCell, null);

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                foreach (Node thisNode in openList)
                {
                    if (thisNode.F < currentNode.F)
                    {
                        currentNode = thisNode;
                    }
                }
                openList.Remove(currentNode);

                Queue<Node> successors = GenerateChildren(currentNode, destinationNode);
                while (successors.Count > 0)
                {
                    Node successor = successors.Dequeue();
                    if (successor.isSameCell(destinationNode))
                    {
                        return GeneratePath(successor);
                    }


                    bool dontAdd = false;
                    foreach (Node openNode in openList)
                    {
                        if (successor.isSameCell(openNode) && openNode.F < successor.F)
                        {
                            dontAdd = true;
                            break;
                        }
                    }

                    if (!dontAdd)
                    {
                        foreach (Node closedNode in closedList)
                        {
                            if (successor.isSameCell(closedNode) && closedNode.F < successor.F)
                            {
                                dontAdd = true;
                                break;
                            }
                        }
                    }

                    if (!dontAdd)
                    {
                        openList.Add(successor);
                    }
                }
                closedList.Add(currentNode);
            }

            return new List<IHexGridCell>();
        }

        private static List<IHexGridCell> GeneratePath(Node finalNode)
        {
            List<IHexGridCell> path = new List<IHexGridCell>();
            Node currentNode = finalNode;

            while (currentNode != null)
            {
                path.Add(currentNode.Cell);
                currentNode = currentNode.Parent;

            }

            path.Reverse();

            return path;
        }


        private static Queue<Node> GenerateChildren(Node parent, Node destination)
        {
            Queue<Node> childrenNodes = new Queue<Node>();

            Point<float> destinationPoint = destination.Cell.Position.Center;
            IHexGridCell[] childrenCells = parent.Cell.Neighbors;

            foreach (IHexGridCell cell in childrenCells)
            {
                if (cell != null && cell.GetComponent<UnselectableComponent>() == null && cell.GetComponent<CreatureComponent>() == null)
                {
                    Point<float> thisPoint = cell.Position.Center;
                    float distanceSquared = SquaredDistanceBetweenFloatPoints(thisPoint, destinationPoint);
                    Node thisNode = new Node(cell, parent, g: 1.0f, h: distanceSquared);
                    childrenNodes.Enqueue(thisNode);
                }
            }

            return childrenNodes;
        }


        private static float SquaredDistanceBetweenFloatPoints(Point<float> first, Point<float> second)
        {
            return ((first.Y - second.Y) * (first.Y - second.Y)) + ((first.X - second.X) + (first.X - second.X));
        }
    }

}