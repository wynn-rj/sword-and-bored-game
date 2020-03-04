using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Strategy.GameResources;

namespace SwordAndBored.Strategy.Squads
{
    public class SquadManager : MonoBehaviour, ISquadManager, ITileSelectSubscriber
    {
        [SerializeField] private SquadController squadPrefab;
        [SerializeField] private TimeTrackingTimeManager turnManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private KeyCode loseSquadFocusKey = KeyCode.Escape;
        [SerializeField] private float squadPlacementHeight = 0;
        [SerializeField] private int squadUpkeepCost = 1;

        private readonly IList<SquadController> squads = new List<SquadController>();

        public SquadController SelectedSquad { get; private set; }

        public SquadController DeploySquad(string name, List<IUnit> units, IHexGridCell location)
        {
            ISquad squad = new Squad(name, units, location.Position.GridPoint.X, location.Position.GridPoint.Y);
            return DeploySquad(squad);
        }

        public IUnit[] CollectSquad(IHexGridCell location)
        {
            SquadController squad = GetSquadOnTile(location);
            if (squad)
            {
                IUnit[] units = squad.SquadData.Units.ToArray();
                squad.SquadData.Delete();
                squads.Remove(squad);
                turnManager.Unsubscribe(squad);
                Destroy(squad);
                return units;
            }
            return null;
        }

        public void OnTileSelect(IHexGridCell selectedTile)
        {
            if (turnManager.IsTimeStepAdvancing) return;
            SquadController squad = GetSquadOnTile(selectedTile);
            if (squad)
            {
                SelectSquad(squad);
                return;
            }

            if (SelectedSquad)
            {
                SelectedSquad.GoTo(selectedTile);
                SelectedSquad = null;
            }
        }

        public void SelectSquad(SquadController squadToSelect)
        {
            SelectedSquad = squadToSelect;
        }

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(squadPrefab, this);
            AssertHelper.IsSetInEditor(turnManager, this);
            AssertHelper.IsSetInEditor(tileManager, this);
            AssertHelper.IsSetInEditor(resourceManager, this);
        }
#endif

        private void Start()
        {
            tileManager.GetComponent<TileSelect>().Subscribe(this);
            foreach (ISquad squad in Squad.GetAllSquads())
            {
                DeploySquad(squad);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(loseSquadFocusKey))
            {
                SelectedSquad = null;
            }
        }

        private void OnDestroy()
        {
            foreach (SquadController controller in squads)
            {
                controller.SquadData.Save();
            }
        }

        private SquadController DeploySquad(ISquad squad)
        {
            SquadController controller = Instantiate(squadPrefab, transform);
            IHexGridCell location = tileManager.HexTiling[squad.X, squad.Y];
            controller.transform.position = location.Position.CenterAsVector3(squadPlacementHeight);
            controller.SquadData = squad;
            controller.StartLocation = location;
            controller.UpkeepFunction = () => resourceManager.GoldAmount -= squadUpkeepCost;
            controller.IsUserControlledFunction = () => !turnManager.IsTimeStepAdvancing;
            controller.SquadData.Save();
            squads.Add(controller);
            turnManager.Subscribe(controller);
            return controller;
        }        

        private SquadController GetSquadOnTile(IHexGridCell tile)
        {
            foreach (SquadController squad in squads)
            {
                if (squad.Location == tile)
                {
                    return squad;
                }
            }
            return null;
        }
    }
}
