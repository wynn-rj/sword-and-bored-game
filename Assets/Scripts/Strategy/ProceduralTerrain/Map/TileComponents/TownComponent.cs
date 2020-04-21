using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Towns;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    class TownComponent : AbstractSelectionComponent
    {
        public ITown Town { get => new Town(town.ID); }
        private readonly ITown town;
        private readonly TownCanvasController townCanvasController;
        private float timeSinceLastClick;

        public TownComponent(ITown town, TownCanvasController townCanvasController)
        {
            this.town = town;
            this.townCanvasController = townCanvasController;
            timeSinceLastClick = Time.time;
        }

        public override void Select()
        {
            float now = Time.time;
            if (now - timeSinceLastClick < 0.5f)
            {
                townCanvasController.DisplayedTown = Town;
            }
            timeSinceLastClick = now;
        }
    }
}
