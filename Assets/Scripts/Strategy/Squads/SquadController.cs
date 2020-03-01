using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.Squads
{
    public class SquadController : GenericSquadController
    {
        public override void GoTo(IHexGridCell location)
        {
            //Make A* call here
            path.Enqueue(location);
            TraversePath();
        }

        protected override int ResetMovement()
        {
            //TODO: Get squad average speed
            return 3;
        }

        protected override bool UpdateLocation(IHexGridCell location)
        {
            CreatureComponent otherCreature = location.GetComponent<CreatureComponent>();
            if (!(otherCreature is null) && !(otherCreature.Creature is EnemyMovementController))
            {
                return false;
            }
            if (Location != null)
            {
                AssertHelper.Assert(creatureComponent != null, "Missing creature component", this);
                bool success = Location.RemoveComponent(creatureComponent);
                AssertHelper.Assert(success, "Failed to remove creature component", this);
            }
            else
            {
                creatureComponent = new CreatureComponent(this);
            }
            Location = location;
            Location.AddComponent(creatureComponent);
            return true;
        }

#if DEBUG 
        protected override void Start()
        {
            base.Start();
            AssertHelper.Assert(Units != null && Units.Length > 0, "Squad controller has no squad", this);
        }
#endif
    }
}