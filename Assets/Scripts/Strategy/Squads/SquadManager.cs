using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Utilities.Debug;

namespace SwordAndBored.Strategy.Squads
{
    public class SquadManager : MonoBehaviour, ISquadManager, ITileSelectSubscriber
    {
        [SerializeField] private SquadController squadPrefab;
        [SerializeField] private TimeTrackingTimeManager turnManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private KeyCode loseSquadFocusKey = KeyCode.Escape;
        [SerializeField] private float squadPlacementHeight = 0;

        private readonly IList<SquadController> squads = new List<SquadController>();

        public GenericSquadController SelectedSquad { get; private set; }

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(squadPrefab, this);
            AssertHelper.IsSetInEditor(turnManager, this);
            AssertHelper.IsSetInEditor(tileManager, this);
        }
#endif

        void Start()
        {
            AssertHelper.IsSetInEditor(tileManager, this);
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

        public GenericSquadController DeploySquad(IUnit[] units, IHexGridCell location)
        {
            SquadController squad = Instantiate(squadPrefab, transform);
            squad.transform.position = location.Position.CenterAsVector3(squadPlacementHeight);
            squad.Units = units;
            squad.StartLocation = location;
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
