using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    abstract class AbstractSelectionComponent : AbstractCellComponent, ISelectionComponent
    {
        public abstract void Select();
    }
}
