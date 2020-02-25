using SwordAndBored.Strategy.GameResources;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    class GoldCityComponent : AbstractSelectionComponent
    {
        public Gold Gold { get; private set; }

        public GoldCityComponent(Gold gold)
        {
            Gold = gold;
        }

        public override void Select()
        {
            Gold.Amount += 100;
        }
    }
}
