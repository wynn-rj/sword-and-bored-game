using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid
{
    public abstract class AbstractCellComponent : ICellComponent
    {
        private IHexGridCell parent;

        public IHexGridCell Parent
        {
            get => parent;
            set
            {
                IHexGridCell oldParent = parent;
                parent = value;
                OnParentSet(oldParent);
            }
        }

        protected virtual void OnParentSet(IHexGridCell oldParent) { }
    }
}