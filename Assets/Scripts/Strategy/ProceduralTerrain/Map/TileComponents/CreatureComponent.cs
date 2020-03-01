using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents
{
    public class CreatureComponent : AbstractCellComponent
    {
        public CreatureMovementController Creature { get; }

        public CreatureComponent(CreatureMovementController creature)
        {
            Creature = creature;
        }
    }
}
