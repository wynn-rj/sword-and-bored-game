using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid
{
    /// <summary>
    /// A component that can be added to a grid cell
    /// </summary>
    public interface ICellComponent
    {
        /// <summary>
        /// The grid cell containing this component
        /// </summary>
        IHexGridCell Parent { get; set; }
    } 
}