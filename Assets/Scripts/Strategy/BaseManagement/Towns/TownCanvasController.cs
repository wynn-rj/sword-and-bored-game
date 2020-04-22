using SwordAndBored.GameData.Units;
using SwordAndBored.SceneManagement;
using SwordAndBored.Strategy.BaseManagement.Units;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.Squads;
using SwordAndBored.Utilities.Random;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Towns
{
    class TownCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject unitEntryPrefab;
        [SerializeField] private SquadManager squadManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private Button deploySquadButton;
        [SerializeField] private Button cancelDeployment;
        [SerializeField] private Button moveToTownButton;
        [SerializeField] private Button moveToSquadButton;
        [SerializeField] private Transform townEntryContainer;
        [SerializeField] private Transform squadEntryContainer;

        private Canvas canvas;
        private ITown displayedTown;
        private List<GameObject> activeEntries = new List<GameObject>();
        private List<GameObject> townEntries = new List<GameObject>();
        private List<GameObject> squadEntries = new List<GameObject>();
        private List<IUnit> squadUnitData = new List<IUnit>();
        private ISquad squad;

        public ITown DisplayedTown
        {
            get => displayedTown;
            set
            {
                displayedTown = value;
                UpdateDisplay();
            }
        }

        private void Awake()
        {
            canvas = GetComponent<Canvas>();

            deploySquadButton.onClick.AddListener(Confirm);
            cancelDeployment.onClick.AddListener(ExitTownCanvas);
            moveToTownButton.onClick.AddListener(MoveUnitToTown);
            moveToSquadButton.onClick.AddListener(MoveUnitToSquad);
            gameObject.SetActive(false);

        }

        private void PreloadCanvas()
        {
            foreach (Transform child in townEntryContainer.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in squadEntryContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void UpdateDisplay()
        {
            if (displayedTown is null || !displayedTown.PlayerOwned) return;

            PreloadCanvas();

            displayedTown.Units.Clear();
            displayedTown.Save();
            activeEntries.Clear();
            townEntries.Clear();
            squadEntries.Clear();
            squadUnitData.Clear();

            foreach (IUnit unit in displayedTown.Units)
            {
                townEntries.Add(CreateTownUnitEntry(unit));
            }

            ISquad squadOnTile = squadManager?.GetSquadOnTile(tileManager.HexTiling[displayedTown.X, displayedTown.Y])?.SquadData;

            if (!(squadOnTile is null) && displayedTown.X == squadOnTile.X && displayedTown.Y == squadOnTile.Y)
            {
                foreach (IUnit unit in squadOnTile.Units)
                {
                    GameObject entry = CreateSquadUnitEntry(unit);
                    squadEntries.Add(entry);
                    squadUnitData.Add(entry.GetComponent<UnitEntryDisplay>().unitEntry.unit);
                }

                squad = squadOnTile;
            }            

            gameObject.SetActive(true);
        }

        public void MoveUnitToTown()
        {
            foreach (GameObject entry in activeEntries)
            {
                if (squadEntries.Contains(entry))
                {
                    IUnit unit = entry.GetComponent<UnitEntryDisplay>().unitEntry.unit;
                    GameObject townEntry = CreateTownUnitEntry(unit);
                    townEntries.Add(townEntry);
                    squadEntries.Remove(entry);
                    squadUnitData.Remove(unit);
                    Destroy(entry);
                }
            }

            activeEntries.Clear();
        }

        public void MoveUnitToSquad()
        {
            foreach (GameObject entry in activeEntries)
            {
                if (townEntries.Contains(entry))
                {
                    IUnit unit = entry.GetComponent<UnitEntryDisplay>().unitEntry.unit;
                    GameObject squadEntry = CreateSquadUnitEntry(unit);
                    squadUnitData.Add(unit);
                    squadEntries.Add(squadEntry);
                    townEntries.Remove(entry);
                    Destroy(entry);
                }
            }

            activeEntries.Clear();
        }

        private GameObject CreateTownUnitEntry(IUnit unit)
        {
            GameObject unitEntryObject = CreateUnitEntry(unit);
            unitEntryObject.transform.SetParent(townEntryContainer.transform);

            return unitEntryObject;
        }

        private GameObject CreateSquadUnitEntry(IUnit unit)
        {
            GameObject unitEntryObject = CreateUnitEntry(unit);
            unitEntryObject.transform.SetParent(squadEntryContainer.transform);

            return unitEntryObject;
        }

        private GameObject CreateUnitEntry(IUnit unit)
        {
            UnitEntry unitEntryData = UnitEntry.CreateInstance(unit);

            GameObject unitEntryObject = Instantiate(unitEntryPrefab) as GameObject;
            unitEntryObject.transform.localRotation = Quaternion.identity;
            unitEntryObject.transform.localScale = new Vector3(.8f, 1, .8f);

            unitEntryObject.GetComponent<UnitEntryDisplay>().unitEntry = unitEntryData;
            unitEntryObject.GetComponent<UnitEntryDisplay>().SetDisplay();
            unitEntryObject.GetComponent<ToggleUnitToSquad>().Initialize(AddUnitToSquad, RemoveUnitFromSquad);

            return unitEntryObject;
        }

        public void ExitTownCanvas()
        {
            gameObject.SetActive(false);
        }
        
        public void AddUnitToSquad(GameObject unitEntry)
        {
            activeEntries.Add(unitEntry);
        }

        public void RemoveUnitFromSquad(GameObject unitEntry)
        {
            activeEntries.Remove(unitEntry);
        }

        public void Confirm()
        {
            List<IUnit> townUnits = new List<IUnit>();
            foreach (GameObject entryObject in townEntries)
            {
                townUnits.Add(entryObject.GetComponent<UnitEntryDisplay>().unitEntry.unit);
            }

            displayedTown.Units = townUnits;
            displayedTown.Save();

            if (squad == null)
            {
                if ((squadEntries.Count > 0))
                {
                    DeploySquad();
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (squadEntries.Count < 1)
                {
                    squadManager.CollectSquad(squad.X, squad.Y);
                }
                else
                {
                    squad.Units = squadUnitData;
                    squad.Save();
                }
            }

            squad = null;
            activeEntries.Clear();
            gameObject.SetActive(false);
        }

        public void DeploySquad()
        {
            IList<IHexGridCell> cellNeighbors = tileManager.HexTiling.CellNeighbors(displayedTown.X, displayedTown.Y);

            IHexGridCell location = Odds.SelectAtRandom<IHexGridCell>(cellNeighbors);

            while (location.HasComponent<CreatureComponent>())
            {
                location = Odds.SelectAtRandom<IHexGridCell>(cellNeighbors);
            }

            List<IUnit> deployedSquad = new List<IUnit>();

            foreach (IUnit unit in squadUnitData)
            {
                deployedSquad.Add(unit);
            }

            squadManager.DeploySquad("Name", deployedSquad, location);
        }

        public void ManageBase()
        {
            SceneManager.LoadSceneAsync(GameScenes.BASEVIEW);
        }
    }
}
