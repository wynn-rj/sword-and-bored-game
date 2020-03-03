using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Units;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class Barracks : GenericBuilding
    {
        private IList<IUnit> activeUnitsList;

        [SerializeField] private GameObject unitEntryPrefab;
        [SerializeField] private GameObject trainUnitCanvas;
        [SerializeField] private GameObject unitListPanel;
        [SerializeField] private DisplayModelController displayModelController;
        [SerializeField] private DisplayDataController displayDataController;

        public GameObject TrainUnitCanvas
        {
            get { return trainUnitCanvas; }
        }

        private void Awake()
        {
            UnitManager.Instance.GetAllData();
        }

        void Start()
        {
            /*
             * TO DO: Get all active units from database and save to list
             */

            activeUnitsList = UnitManager.Instance.GetAllUnits();

            /*
             * TO DO: Create entries for all units, and place in GUI display
             */

            foreach (IUnit unit in activeUnitsList)
            {
                CreateUnitEntry(unit);
            }

            IUnit warrior = new Unit("Warrior");
            warrior.Name = "Bob";
            IUnit scout = new Unit("Scout");
            scout.Name = "Joe";
            IUnit mage = new Unit("Mage");
            mage.Name = "Sally";
            CreateUnitEntry(warrior);
            CreateUnitEntry(scout);
            CreateUnitEntry(mage);
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

        /// <summary>
        /// Adds a UI element to scene based off <para>unit</para> data
        /// </summary>
        /// <param name="unit"></param>
        void CreateUnitEntry(IUnit unit)
        {
            /*
             * TO DO: Construct unit entry, add to GUI display
             */

            // Note arg placeholders
            UnitEntry unitEntryData = UnitEntry.CreateInstance(0, unit, null);

            GameObject unitEntryObject = Instantiate(unitEntryPrefab) as GameObject;
            unitEntryObject.transform.SetParent(unitListPanel.transform);
            unitEntryObject.transform.localRotation = Quaternion.identity;
            unitEntryObject.transform.localScale = new Vector3(2, 1, .8f);
            unitEntryObject.GetComponent<UnitEntryDisplay>().UnitEntry = unitEntryData;
            unitEntryObject.GetComponent<UnitEntryDisplay>().SetDisplay();
            unitEntryObject.GetComponent<ShowEntryModel>().Initialize(displayModelController.GetModel(unit.Role.Name), displayModelController.EnableModel);
            unitEntryObject.GetComponent<ShowEntryData>().Initialize(unit, displayDataController.SetData);
        }
    }
}
