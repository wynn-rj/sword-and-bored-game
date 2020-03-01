using System;
using System.Collections.Generic;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid
{
    /// <summary>
    /// A grid of hexagon cells
    /// </summary>
    public class HexGrid
    {
        private readonly IHexGridCell[,] gridCells;
        private readonly int xDim;
        private readonly int yDim;

        /// <summary>
        /// An enumeration of all cells found in the grid.
        /// </summary>
        public IEnumerable<IHexGridCell> AllCells
        {
            get
            {
                foreach (IHexGridCell item in gridCells)
                    yield return item;
            }
        }

        /// <summary>
        /// Creates a new grid of hex cells. Each cell is initialized as an empty cell 
        /// to which components can be added to
        /// </summary>
        /// <param name="cellRadius">The radius of a single hexagon (The distance from 
        /// the center to a corner)</param>
        /// <param name="width">The total width of grid</param>
        /// <param name="height">The total height of the grid</param>
        public HexGrid(float cellRadius, float width, float height)
        {
            xDim = (int)Math.Ceiling(width / 2);
            yDim = (int)Math.Ceiling(width / 2);
            gridCells = new IHexGridCell[2 * xDim + 1, 2 * yDim + 1];
            for (int x = -xDim; x <= xDim; x++) {
                for (int y = -yDim; y <= yDim; y++) {
                    gridCells[x + xDim, y + yDim] = new EmptyGridCell(x, y, cellRadius, this);
                }
            }
        }

        /// <summary>
        /// Returns the 6 cells surrounding a cell
        /// </summary>
        /// <param name="cell">The position of the cell</param>
        /// <returns>The 6 cells surrounding a cell</returns>
        public IHexGridCell[] CellNeighbors(IHexGridCell cell) => CellNeighbors(cell.Position.GridPoint);

        /// <summary>
        /// Returns the 6 cells surrounding a cell
        /// </summary>
        /// <param name="pos">The position of the cell</param>
        /// <returns>The 6 cells surrounding a cell</returns>
        public IHexGridCell[] CellNeighbors(Point<int> pos) => CellNeighbors(pos.X, pos.Y);

        /// <summary>
        /// Returns the 6 cells surrounding a cell
        /// </summary>
        /// <param name="x">The x index of the cell</param>
        /// <param name="y">The y index of the cell</param>
        /// <returns>The 6 cells surrounding a cell</returns>
        public IHexGridCell[] CellNeighbors(int x, int y)
        {
            int yShift = (x % 2 == 0) ? -1 : 1;
            return new IHexGridCell[] {
                GetCell(x, y + 1), 
                GetCell(x, y - 1),
                GetCell(x - 1, y),
                GetCell(x - 1, y + yShift),
                GetCell(x + 1, y),
                GetCell(x + 1, y + yShift)
            };
        }

        /// <summary>
        /// Gets a cell by its grid index
        /// </summary>
        /// <param name="x">The x index of the cell</param>
        /// <param name="y">The y index of the cell</param>
        /// <returns>The cell at the given indexes</returns>
        public IHexGridCell this[int x, int y]
        {
            get => GetCell(x, y);
        }

        public IHexGridCell this[Point<int> p]
        {
            get => GetCell(p.X, p.Y);
        }

        private IHexGridCell GetCell(int x, int y)
        {
            if (Math.Abs(x) > xDim || Math.Abs(y) > yDim)
            {
                return null;
            }

            return gridCells[x + xDim, y + yDim];
        }
    }
}
