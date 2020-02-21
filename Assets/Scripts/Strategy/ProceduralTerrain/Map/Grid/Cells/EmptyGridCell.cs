using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells
{
    public class EmptyGridCell: IHexGridCell
    {
        public HexPoint Position { get; }

        public IEnumerable<ICellComponent> Components => ComponentList;

        private List<ICellComponent> ComponentList;
        private NestingSelectionActionComponent selectionComponents;

        public EmptyGridCell(int x, int y, float gridRadius)
        {
            Position = new HexPoint(x, y, gridRadius);
            ComponentList = new List<ICellComponent>();
            selectionComponents = new NestingSelectionActionComponent();
            ComponentList.Add(selectionComponents);
            selectionComponents.Parent = this;
        }

        public void AddComponent(ICellComponent component)
        {
            if (component is ISelectionComponent selectionComponent)
            {
                selectionComponents.InternalComponents.Add(selectionComponent);
            }
            else
            {
                ComponentList.Add(component);
            }
            component.Parent = this;
        }

        public bool RemoveComponent(ICellComponent component)
        {
            if (component is ISelectionComponent selectionComponent)
            {
                return selectionComponents.InternalComponents.Remove(selectionComponent);
            }
            return ComponentList.Remove(component);
        }

        public bool RemoveComponent<T>() where T : ICellComponent
        {
            if (typeof(T) is ISelectionComponent)
            {
                int length = selectionComponents.InternalComponents.Count;
                selectionComponents.InternalComponents.Clear();
                return length != 0;
            }
            return ComponentList.Remove(GetComponent<T>());
        }        

        public T GetComponent<T>() where T : ICellComponent
        {
            foreach (ICellComponent component in ComponentList)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return default(T);
        }
    }
}