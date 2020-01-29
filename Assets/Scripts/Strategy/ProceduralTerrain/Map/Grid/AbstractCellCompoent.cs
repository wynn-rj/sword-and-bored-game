using SwordAndBored.StrategyView.Map.Grid.Cells;

namespace SwordAndBored.StrategyView.Map.Grid
{
    public abstract class AbstractCellComponent : ICellComponent
    {
        public IHexGridCell Parent { get; set; }
    }
}