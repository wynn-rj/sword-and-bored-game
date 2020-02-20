using System.Collections.Generic;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells
{
    /// <summary>
    /// A grid cell on a hex grid. Contains a series of components that can be added to.
    /// </summary>
    public interface IHexGridCell
    {
        /// <summary>
        /// The position and center of the hex grid
        /// </summary>
        HexPoint Position { get; }

        /// <summary>
        /// An enumeration of all the components attached to the cell
        /// </summary>
        IEnumerable<ICellComponent> Components { get; }

        /// <summary>
        /// Add a component to the grid cell
        /// </summary>
        /// <param name="component">The component to add</param>
        void AddComponent(ICellComponent component);

        /// <summary>
        /// Gets a component of the type T that is attached to the grid cell
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve</typeparam>
        /// <returns>The component of type T</returns>
        T GetComponent<T>() where T : ICellComponent;
    } 
}