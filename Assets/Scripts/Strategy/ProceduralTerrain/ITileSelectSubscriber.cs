using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public interface ITileSelectSubscriber
    {
        void OnTileSelect(IHexGridCell selectedTile);
    }
}
