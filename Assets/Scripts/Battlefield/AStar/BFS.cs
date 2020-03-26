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
        public NodeB parent;

        public NodeB(NodeB parent, int x, int y, int distance)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;
            this.distance = distance;
        }

    }

    public class BFS
    {
        public List<Tile> GetPossibleMove(Tile[,] grid, MovementSystem ms, int movement)
        {
            int movementLeft = movement;
            List<NodeB> Q = new List<NodeB>();
            List<NodeB> visitedList = new List<NodeB>();
            NodeB s = new NodeB(null, ms.currentTile.x, ms.currentTile.y, movementLeft);
            Q.Add(s);
            visitedList.Add(s);
            while (Q.Count > 0)
            {

                NodeB v = Q[Q.Count - 1];
                Q.RemoveAt(Q.Count - 1);
                NodeB up = new NodeB(v, v.x, v.y + 1, v.distance - 1);
                checkValidity(grid, Q, visitedList, up);
                NodeB down = new NodeB(v, v.x, v.y - 1, v.distance - 1);
                checkValidity(grid, Q, visitedList, down);
                NodeB left = new NodeB(v, v.x - 1, v.y, v.distance - 1);
                checkValidity(grid, Q, visitedList, left);
                NodeB right = new NodeB(v, v.x + 1, v.y, v.distance - 1);
                checkValidity(grid, Q, visitedList, right);

            }

            List<Tile> list = new List<Tile>();
            foreach (NodeB node in Q)
            {
                list.Add(grid[node.x, node.y]);
            }

            return list;
        }

        private static void checkValidity(Tile[,] grid, List<NodeB> Q, List<NodeB> visitedList, NodeB currentNode)
        {
            if (currentNode.distance > 0 && grid[currentNode.x, currentNode.y] && grid[currentNode.x, currentNode.y].walkable && !grid[currentNode.x, currentNode.y].unitOnTile)
            {
                if  (Mathf.Abs(currentNode.x) < 49 && Mathf.Abs(currentNode.y) < 49 && Mathf.Abs(currentNode.x) > 0 && Mathf.Abs(currentNode.y) > 0)
                {
                    bool visit = false;
                    foreach (NodeB node in visitedList)
                    {
                        if (node == currentNode)
                        {
                            visit = true;
                        }
                    }
                    if (!visit)
                    {
                        Q.Add(currentNode);
                        visitedList.Add(currentNode);
                    }
                }
            }
        }
    }




    //public class BFS
    //{

    //    public List<Tile> FindMoveZone(Tile goal, Tile[,] grid, MovementSystem ms)
    //    {
    //        List<BFSNode> openList = new List<BFSNode>();
    //        List<BFSNode> closedList = new List<BFSNode>();

    //        BFSNode start = new BFSNode(null, ms.currentTile.x, ms.currentTile.y);
    //        start.g = start.h = start.f = 0;
    //        BFSNode end = new BFSNode(null, goal.x, goal.y);
    //        end.g = end.h = end.f = 0;

    //        openList.Add(start);

    //        while (openList.Count > 0)
    //        {
    //            BFSNode currentNode = openList[0];
    //            int currentIndex = 0;
    //            for (int i = 0; i < openList.Count; i++)
    //            {
    //                if (openList[i].f < currentNode.f)
    //                {
    //                    currentNode = openList[i];
    //                    currentIndex = i;
    //                }
    //            }

    //            openList.RemoveAt(currentIndex);
    //            closedList.Add(currentNode);

    //            if (currentNode.x == end.x && currentNode.y == end.y)
    //            {
    //                List<Tile> path = new List<Tile>();
    //                BFSNode current = currentNode;
    //                while (current != null)
    //                {
    //                    path.Add(grid[current.x, current.y]);
    //                    current = current.parent;
    //                }
    //                return path;
    //            }

    //            List<BFSNode> children = new List<BFSNode>();

    //            //check surronding tiles
    //            BFSNode nodePos = new BFSNode(currentNode, currentNode.x + 1, currentNode.y);
    //            if (nodePos.x < 49 && nodePos.y < 49)
    //            {
    //                if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
    //                {
    //                    children.Add(nodePos);
    //                }
    //            }
    //            nodePos = new BFSNode(currentNode, currentNode.x, currentNode.y + 1);
    //            if (nodePos.x < 49 && nodePos.y < 49)
    //            {
    //                if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
    //                {
    //                    children.Add(nodePos);
    //                }
    //            }
    //            nodePos = new BFSNode(currentNode, currentNode.x - 1, currentNode.y);
    //            if (nodePos.x < 49 && nodePos.y < 49)
    //            {
    //                if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
    //                {
    //                    children.Add(nodePos);
    //                }
    //            }
    //            nodePos = new BFSNode(currentNode, currentNode.x, currentNode.y - 1);
    //            if (nodePos.x < 49 && nodePos.y < 49)
    //            {
    //                if (grid[nodePos.x, nodePos.y] && grid[nodePos.x, nodePos.y].walkable && !grid[nodePos.x, nodePos.y].unitOnTile)
    //                {
    //                    children.Add(nodePos);
    //                }
    //            }

    //            foreach (BFSNode child in children)
    //            {
    //                bool stop = false;
    //                foreach (BFSNode closedChild in closedList)
    //                {
    //                    if (!stop && child == closedChild)
    //                    {
    //                        stop = true;
    //                    }
    //                }
    //                if (!stop)
    //                {
    //                    child.g = currentNode.g + 1;
    //                    child.h = Mathf.RoundToInt(Vector3.Distance(goal.transform.position, grid[child.x, child.y].transform.position));
    //                    child.f = child.g + child.h;

    //                    foreach (BFSNode openNode in openList)
    //                    {
    //                        if (!stop && child == openNode && child.g > openNode.g)
    //                        {
    //                            stop = true;
    //                        }
    //                    }
    //                    if (!stop)
    //                    {
    //                        openList.Add(child);
    //                    }
    //                }
    //            }
    //        }
        
    //        return null;
    //    }
    //}
}
