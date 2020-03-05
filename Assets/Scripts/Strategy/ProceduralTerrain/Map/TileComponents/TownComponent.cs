using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Towns;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    class TownComponent : AbstractSelectionComponent
    {
        private readonly ITown town;
        private readonly TownCanvasController townCanvasController;

        public TownComponent(ITown town, TownCanvasController townCanvasController)
        {
            this.town = town;
            this.townCanvasController = townCanvasController;
        }

        public override void Select()
        {
            townCanvasController.DisplayedTown = town;
        }
    }
}
