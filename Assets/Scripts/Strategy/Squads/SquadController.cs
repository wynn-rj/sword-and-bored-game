using SwordAndBored.Strategy.GameResources;
using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.Debug;
using System;

namespace SwordAndBored.Strategy.Squads
{
    public class SquadController : GenericSquadController
    {
        private bool wipePath;
        private bool performUpkeep;

        public Action UpkeepFunction { private get; set; }
        public Func<bool> IsUserControlledFunction { private get; set; }

        public override void PreTimeStepUpdate()
        {
            base.PreTimeStepUpdate();
            wipePath = true;
            performUpkeep = true;
        }

        public override void GoTo(IHexGridCell location)
        {
            //Make A* call here
            if (wipePath)
            {
                path.Clear();
                wipePath = false;
            }
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
            if (!(otherCreature is null) &&
                !(otherCreature.Creature is EnemyMovementController && IsUserControlledFunction()))
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
            AssertHelper.Assert(UpkeepFunction != null, "Squad has no resource manager", this);
        }
#endif

        protected override void Update()
        {
            base.Update();
            if (performUpkeep)
            {
                UpkeepFunction();
                performUpkeep = false;
            }
        }
    }
}
