using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Units;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.Squads;
using SwordAndBored.Utilities.Random;
using System.Collections.Generic;
using UnityEngine;
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
        [SerializeField] private Transform entryContainer;

        private Canvas canvas;
        private ITown displayedTown;
        private List<IUnit> deployedSquad;

        public ITown DisplayedTown
        {
            get => displayedTown;
            set
            {
                displayedTown = value;
                UpdateDisplay();
            }
        }

        private void Start()
        {
            deployedSquad = new List<IUnit>();
            canvas = GetComponent<Canvas>();

            deploySquadButton.onClick.AddListener(DeploySquad);
            cancelDeployment.onClick.AddListener(ExitTownCanvas);
        }

        private void UpdateDisplay()
        {
            foreach (Transform child in entryContainer.transform)
            {
                Destroy(child.gameObject);
            }

            if (displayedTown is null) return;

            foreach (IUnit unit in displayedTown.Units)
            {
                CreateTownUnitEntry(unit);
            }

            gameObject.SetActive(true);
        }

        private GameObject CreateTownUnitEntry(IUnit unit)
        {
            UnitEntry unitEntryData = UnitEntry.CreateInstance(unit);

            GameObject unitEntryObject = Instantiate(unitEntryPrefab) as GameObject;
            unitEntryObject.transform.SetParent(entryContainer.transform);
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

        public void AddUnitToSquad(IUnit unit)
        {
            deployedSquad.Add(unit);
        }

        public void RemoveUnitFromSquad(IUnit unit)
        {
            deployedSquad.Remove(unit);
        }

        public void DeploySquad()
        {
            IList<IHexGridCell> cellNeighbors = tileManager.HexTiling.CellNeighbors(displayedTown.X, displayedTown.Y);
            foreach (IUnit unit in deployedSquad)
            {
                Debug.Log(unit.Name);
            }
            Debug.Log(deployedSquad.Count);
            /*do
            {

            } while ();*/

            squadManager.DeploySquad("Name", deployedSquad, Odds.SelectAtRandom<IHexGridCell>(cellNeighbors));
            deployedSquad.Clear();
            gameObject.SetActive(false);
        }
    }
}
