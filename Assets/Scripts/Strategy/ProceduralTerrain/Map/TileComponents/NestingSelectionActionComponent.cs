using System.Collections.Generic;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    class NestingSelectionActionComponent : AbstractSelectionComponent
    {
        public IList<ISelectionComponent> InternalComponents { get; } = new List<ISelectionComponent>();
        public override void Select()
        {
            foreach (ISelectionComponent selectionComponent in InternalComponents)
            {
                selectionComponent.Select();
            }
        }
    }
}
