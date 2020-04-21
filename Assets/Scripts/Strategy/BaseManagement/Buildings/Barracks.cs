using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Units;
using SwordAndBored.StrategyView.BaseManagement.Buildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SwordAndBored.Strategy.Inventory;

namespace SwordAndBored.Strategy.BaseManagement.Buildings
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

        [HideInInspector] public GameObject activeEntry;

        public GameObject TrainUnitCanvas => trainUnitCanvas;

        private void Awake()
        {
            UnitManager.Instance.GetAllData();
            unitEntriesList = new List<GameObject>();
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

        private void Update()
        {
            dispatchUnitButton.interactable = activeEntry.GetComponent<UnitEntryDisplay>().unitEntry.currentTown == "Player Base";
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
            dispatchUnitCanvas.SetActive(true);
        }

        public void ExitDispatchUnitCanvas()
        {
            dispatchUnitCanvas.SetActive(false);
        }

        public void SetActiveEntry(GameObject unitEntry)
        {
            activeEntry.gameObject.GetComponent<Image>().color = Color.white;
            activeEntry = unitEntry;
            activeEntry.gameObject.GetComponent<Image>().color = Color.grey;
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
            unitEntryObject.transform.localScale = new Vector3(1, 1, .8f);

            unitEntryObject.GetComponent<UnitEntryDisplay>().unitEntry = unitEntryData;
            unitEntryObject.GetComponent<UnitEntryDisplay>().SetDisplay();
            unitEntryObject.GetComponent<ShowEntryModel>().Initialize(displayModelController.GetModel(unit.Role.Name), displayModelController.EnableModel);
            unitEntryObject.GetComponent<ShowEntryData>().Initialize(unit, displayDataController.SetData);
            unitEntryObject.GetComponent<SetActiveEntry>().Initialize(SetActiveEntry);

            return unitEntryObject;
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

        public void OpenInventory()
        {
            MonoInventory.Instance.OpenInventory(activeEntry.GetComponent<UnitEntryDisplay>().unitEntry.unit);
        }
    }

}
