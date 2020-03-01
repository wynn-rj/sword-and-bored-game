using SwordAndBored.Strategy.GameResources;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    class GoldCityComponent : AbstractSelectionComponent
    {
        public ResourceManager ResourceManager { get; private set; }

        public GoldCityComponent(ResourceManager resourceManager)
        {
            ResourceManager = resourceManager;
        }

        public override void Select()
        {
            ResourceManager.GoldAmount += 100;
        }
    }
}
