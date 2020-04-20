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
        private List<GameObject> activeEntries;
        private List<GameObject> townEntries = new List<GameObject>();
        private List<GameObject> squadEntries;
        private List<IUnit> squadUnitData;
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
            townEntries = new List<GameObject>();
            squadEntries = new List<GameObject>();
            squadUnitData = new List<IUnit>();
            activeEntries = new List<GameObject>();
            canvas = GetComponent<Canvas>();

            deploySquadButton.onClick.AddListener(Confirm);
            cancelDeployment.onClick.AddListener(ExitTownCanvas);
            moveToTownButton.onClick.AddListener(MoveUnitToTown);
            moveToSquadButton.onClick.AddListener(MoveUnitToSquad);
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

            foreach (IUnit unit in displayedTown.Units)
            {
                townEntries.Add(CreateTownUnitEntry(unit));
            }

            gameObject.SetActive(true);
        }

        public void LoadCanvas()
        {
            PreloadCanvas();

            displayedTown.Units.Clear();
            displayedTown.Save();
            if (displayedTown is null) return;

            foreach (IUnit unit in displayedTown.Units)
            {
                townEntries.Add(CreateTownUnitEntry(unit));
            }

            foreach (IUnit unit in squadManager.SelectedSquad.SquadData.Units)
            {
                GameObject entry = CreateSquadUnitEntry(unit);
                squadEntries.Add(entry);
                squadUnitData.Add(entry.GetComponent<UnitEntryDisplay>().unitEntry.unit);
            }

            squad = squadManager.SelectedSquad.SquadData;

            gameObject.SetActive(true);
        }

        public void MoveUnitToTown()
        {
            foreach (GameObject entry in activeEntries)
            {
                if (squadEntries.Contains(entry))
                {
                    GameObject townEntry = CreateTownUnitEntry(entry.GetComponent<UnitEntryDisplay>().unitEntry.unit);
                    townEntries.Add(townEntry);
                    squadEntries.Remove(entry);
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
                    GameObject squadEntry = CreateSquadUnitEntry(entry.GetComponent<UnitEntryDisplay>().unitEntry.unit);
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
            unitEntryObject.transform.localScale = new Vector3(1, 1, .8f);

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
                DeploySquad();
            }
            else
            {
                squad.Units = squadUnitData;
                squad.Save();
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

            foreach (GameObject entry in activeEntries)
            {
                deployedSquad.Add(entry.GetComponent<UnitEntryDisplay>().unitEntry.unit);
            }

            squadManager.DeploySquad("Name", deployedSquad, location);
        }

        public void ManageBase()
        {
            SceneManager.LoadSceneAsync(GameScenes.BASEVIEW);
        }
    }
}
