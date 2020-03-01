using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;

namespace SwordAndBored.Strategy.Squads
{
    public abstract class GenericSquadController : CreatureMovementController
    {
        public IUnit[] Units { get; set; }

        public abstract void GoTo(IHexGridCell location);
    }
}
