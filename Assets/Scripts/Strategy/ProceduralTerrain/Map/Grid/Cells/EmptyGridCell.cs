using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells
{
    public class EmptyGridCell: IHexGridCell
    {
        public HexPoint Position { get; }
        public HexGrid ParentGrid { get; }
        public IHexGridCell[] Neighbors => ParentGrid.CellNeighbors(this);

        public IEnumerable<ICellComponent> Components => componentList;

        private readonly List<ICellComponent> componentList;
        private readonly NestingSelectionActionComponent selectionComponents;

        public EmptyGridCell(int x, int y, float gridRadius, HexGrid parentGrid)
        {
            Position = new HexPoint(x, y, gridRadius);
            componentList = new List<ICellComponent>();
            selectionComponents = new NestingSelectionActionComponent();
            selectionComponents.Parent = this;
            ParentGrid = parentGrid;
        }

        public void AddComponent(ICellComponent component)
        {
            if (component is ISelectionComponent selectionComponent)
            {
                if (selectionComponents.InternalComponents.Count == 0)
                {
                    componentList.Add(selectionComponents);
                }
                selectionComponents.InternalComponents.Add(selectionComponent);
            }
            else
            {
                componentList.Add(component);
            }
            component.Parent = this;
        }

        public bool RemoveComponent(ICellComponent component)
        {
            if (component is ISelectionComponent selectionComponent)
            {
                bool success = selectionComponents.InternalComponents.Remove(selectionComponent);
                if (success && selectionComponents.InternalComponents.Count == 0)
                {
                    componentList.Remove(selectionComponents);
                }
                return success;
            }
            return componentList.Remove(component);
        }

        public bool RemoveComponent<T>() where T : ICellComponent
        {
            if (typeof(T) is ISelectionComponent)
            {
                if (selectionComponents.InternalComponents.Count > 0)
                {
                    selectionComponents.InternalComponents.Clear();
                    componentList.Remove(selectionComponents);
                    return true;
                }
                return false;
            }
            return componentList.Remove(GetComponent<T>());
        }        

        public T GetComponent<T>() where T : ICellComponent
        {
            foreach (ICellComponent component in componentList)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            foreach (ICellComponent component in selectionComponents.InternalComponents)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return default;
        }

        public bool HasComponent(ICellComponent component)
        {
            bool storedInInternal = false;
            if (component is ISelectionComponent selectionComponent)
            {
                storedInInternal = selectionComponents.InternalComponents.Contains(selectionComponent);
            }
            return storedInInternal || componentList.Contains(component);
        }

        public bool HasComponent<T>() where T : ICellComponent
        {
            return !Equals(GetComponent<T>(), default);
        }
    }
}