
using System.Collections.Generic;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells
{
    public class AStarModule
    {

        class Node
        {
            public IHexGridCell Cell { get; }
            public IHexGridCell Parent { get; }

            public Node(IHexGridCell cell, IHexGridCell parent)
            {
                Cell = cell;
                Parent = parent;
            }
        }

        public Stack<IHexGridCell> FindPath(IHexGridCell startCell, IHexGridCell destinationCell)
        {
            return new Stack<IHexGridCell>();
        }
    }
}
