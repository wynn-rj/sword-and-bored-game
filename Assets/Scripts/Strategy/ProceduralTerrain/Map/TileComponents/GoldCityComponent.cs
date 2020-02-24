using SwordAndBored.Strategy.GameResources;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    public class GoldCityComponent : Grid.AbstractCellComponent
    {
        public Gold Gold { get; private set; }

        public GoldCityComponent(Gold gold)
        {
            Gold = gold;
        }
    }
}
