using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid
{
    public abstract class AbstractCellComponent : ICellComponent
    {
        public IHexGridCell Parent { get; set; }
    }
}