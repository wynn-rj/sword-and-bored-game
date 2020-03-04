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

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(squadPrefab, this);
            AssertHelper.IsSetInEditor(turnManager, this);
            AssertHelper.IsSetInEditor(tileManager, this);
            AssertHelper.IsSetInEditor(resourceManager, this);
        }
#endif

        void Start()
        {
            tileManager.GetComponent<TileSelect>().Subscribe(this);
            ProceduralTerrain.Map.Grid.HexGrid map = tileManager.HexTiling;
            DeploySquad(new IUnit[1], map[0, 0]);
            DeploySquad(new IUnit[1], map[1, 0]);
            DeploySquad(new IUnit[1], map[2, 0]);
        }

        void Update()
        {
            if (Input.GetKeyDown(loseSquadFocusKey))
            {
                SelectedSquad = null;
            }
        }

        public SquadController DeploySquad(ISquad squad)
        {
            SquadController squad = Instantiate(squadPrefab, transform);
            squad.transform.position = location.Position.CenterAsVector3(squadPlacementHeight);
            squad.Units = units;
            squad.StartLocation = location;
            squad.UpkeepFunction = () => resourceManager.GoldAmount -= squadUpkeepCost;
            squad.IsUserControlledFunction = () => !turnManager.IsTimeStepAdvancing;
            squads.Add(squad);
            turnManager.Subscribe(squad);
            return squad;
        }

        public IUnit[] CollectSquad(IHexGridCell location)
        {
            SquadController squad = GetSquadOnTile(location);
            if (squad)
            {
                IUnit[] units = squad.Units;
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
