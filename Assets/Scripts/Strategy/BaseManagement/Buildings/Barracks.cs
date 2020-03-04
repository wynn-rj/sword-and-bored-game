using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class Barracks : GenericBuilding
    {
        [SerializeField] private GameObject unitEntryPrefab;
        [SerializeField] private GameObject trainUnitCanvas;
        [SerializeField] private GameObject dispatchUnitCanvas;
        [SerializeField] private GameObject unitListPanel;

        [SerializeField] private Button trainUnitButton;
        [SerializeField] private Button dispatchUnitButton;

        [SerializeField] private DisplayModelController displayModelController;
        [SerializeField] private DisplayDataController displayDataController;

        private IList<IUnit> activeUnitsList;
        private IList<GameObject> unitEntriesList;

        private GameObject activeEntry;

        public GameObject TrainUnitCanvas => trainUnitCanvas;

        private void Awake()
        {
            UnitManager.Instance.GetAllData();
            unitEntriesList = new List<GameObject>();

            //trainUnitButton.onClick.AddListener();
        }

        private void Start()
        {
            activeUnitsList = UnitManager.Instance.GetAllUnits();

            foreach (IUnit unit in activeUnitsList)
            {
                GameObject entry = CreateUnitEntry(unit);
                unitEntriesList.Add(entry);
            }

            activeEntry = unitEntriesList[0];
        }

        public void EnterTrainUnitCanvas()
        {
            MainCanvas.SetActive(false);
            trainUnitCanvas.SetActive(true);
        }

        public void ExitTrainUnitCanvas()
        {
            MainCanvas.SetActive(true);
            trainUnitCanvas.SetActive(false);
        }

        public void EnterDispatchUnitCanvas()
        {
            MainCanvas.SetActive(false);
            dispatchUnitCanvas.SetActive(true);
        }

        public void ExitDispatchUnitCanvas()
        {
            MainCanvas.SetActive(true);
            dispatchUnitCanvas.SetActive(false);
        }

        public void SetActiveEntry(GameObject unitEntry)
        {
            activeEntry = unitEntry;
        }

        /// <summary>
        /// Adds a UI element to scene based off <para>unit</para> data
        /// </summary>
        /// <param name="unit"></param>
        public GameObject CreateUnitEntry(IUnit unit)
        {
            UnitEntry unitEntryData = UnitEntry.CreateInstance(unit);

            GameObject unitEntryObject = Instantiate(unitEntryPrefab) as GameObject;
            unitEntryObject.transform.SetParent(unitListPanel.transform);
            unitEntryObject.transform.localRotation = Quaternion.identity;
            unitEntryObject.transform.localScale = new Vector3(2, 1, .8f);

            unitEntryObject.GetComponent<UnitEntryDisplay>().unitEntry = unitEntryData;
            unitEntryObject.GetComponent<UnitEntryDisplay>().SetDisplay();
            unitEntryObject.GetComponent<ShowEntryModel>().Initialize(displayModelController.GetModel(unit.Role.Name), displayModelController.EnableModel);
            unitEntryObject.GetComponent<ShowEntryData>().Initialize(unit, displayDataController.SetData);
            unitEntryObject.GetComponent<SetActiveEntry>().Initialize(SetActiveEntry);

            return unitEntryObject;
        }

        public void DispatchUnit(ITown town)
        {
            UnitEntry entryData = activeEntry.GetComponent<UnitEntryDisplay>().unitEntry;
            IUnit dispatchableUnit = entryData.unit;
            dispatchableUnit.Town = town;
            dispatchableUnit.Save();
        }

        /// <summary>
        /// Sorts list of gameobject entries based off provided metric
        /// </summary>
        public void SortEntries(string metric)
        {
            foreach (Transform child in unitListPanel.transform)
            { 
                /*
                 * Destroy all children
                 */
            }

            /*
             * Sort based off metric
             */

            /*
             * Re-add children
             */
        }
    }
}
