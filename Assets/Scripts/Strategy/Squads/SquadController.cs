using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.Debug;
using System;

namespace SwordAndBored.Strategy.Squads
{
    public class SquadController : CreatureMovementController
    {
        private bool wipePath;
        private bool performUpkeep;
        private int squadAverageMovement;

        public ISquad SquadData { get; set; }
        public Action UpkeepFunction { private get; set; }
        public Func<bool> IsUserControlledFunction { private get; set; }

        public override void PreTimeStepUpdate()
        {
            base.PreTimeStepUpdate();
            wipePath = true;
            performUpkeep = true;
        }

        public void GoTo(IHexGridCell location)
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
            return squadAverageMovement;
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

        protected override void Start()
        {
            AssertHelper.Assert(SquadData != null, "Squad controller has no squad", this);
            AssertHelper.Assert(UpkeepFunction != null, "Squad has no resource manager", this);
            squadAverageMovement = SquadData.AverageMovement();
            base.Start();
        }

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
