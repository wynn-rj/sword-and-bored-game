using System;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells
{
    public class EmptyGridCell: IHexGridCell
    {
        public HexPoint Position { get; }

        public IEnumerable<ICellComponent> Components => ComponentList;

        private List<ICellComponent> ComponentList;

        public EmptyGridCell(int x, int y, float gridRadius)
        {
            Position = new HexPoint(x, y, gridRadius);
            ComponentList = new List<ICellComponent>();
        }

        public void AddComponent(ICellComponent component)
        {
            ComponentList.Add(component);
            component.Parent = this;
        }

        public bool RemoveComponent(ICellComponent component)
        {
            return ComponentList.Remove(component);
        }

        public bool RemoveComponent<T>() where T : ICellComponent
        {
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