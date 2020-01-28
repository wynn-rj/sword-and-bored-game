using System;
using System.Collections.Generic;

namespace SwordAndBored.StrategyView.Map.Grid.Cells
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