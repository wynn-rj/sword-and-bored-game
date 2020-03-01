using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain
{
    abstract class AbstractUnselectableTerrainComponent : AbstractTerrainComponent
    {
        protected override void OnParentSet(IHexGridCell oldParent)
        {
            if (oldParent != null)
            {
                oldParent.RemoveComponent<UnselectableComponent>();
            }
            Parent.AddComponent(new UnselectableComponent());
        }
    }
}
