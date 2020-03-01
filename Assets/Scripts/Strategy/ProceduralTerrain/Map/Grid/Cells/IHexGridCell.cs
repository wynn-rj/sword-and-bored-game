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

        HexGrid ParentGrid { get; }

        IHexGridCell[] Neighbors { get; }

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
        /// Remove a component from the grid cell
        /// </summary>
        /// <param name="component">The component to remove</param>
        /// <returns>Whether or not the component was removed</returns>
        bool RemoveComponent(ICellComponent component);

        /// <summary>
        /// Remove a component from the grid cell
        /// </summary>
        /// <typeparam name="T">The type of component to remove</typeparam>
        /// <returns>Whether or not the component was removed</returns>
        bool RemoveComponent<T>() where T : ICellComponent;

        /// <summary>
        /// Gets a component of the type T that is attached to the grid cell
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve</typeparam>
        /// <returns>The component of type T</returns>
        T GetComponent<T>() where T : ICellComponent;

        /// <summary>
        /// Returns whether a cell has a specific component
        /// </summary>
        /// <param name="component">The component to find</param>
        /// <returns>Whether or not the cell has the component</returns>
        bool HasComponent(ICellComponent component);

        /// <summary>
        /// Returns whether a cell has a specific component
        /// </summary>
        /// <typeparam name="T">The type of the component to find</typeparam>
        /// <returns>Whether or not the cell has the component</returns>
        bool HasComponent<T>() where T : ICellComponent;
    } 
}