using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Units;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class Barracks : GenericBuilding
    {
        [SerializeField] private GameObject unitEntryPrefab;
        [SerializeField] private GameObject trainUnitCanvas;
        [SerializeField] private GameObject unitListPanel;
        [SerializeField] private DisplayModelController displayModelController;
        [SerializeField] private DisplayDataController displayDataController;

        private IList<IUnit> activeUnitsList;

        public GameObject TrainUnitCanvas => trainUnitCanvas;

        private void Awake()
        {
            UnitManager.Instance.GetAllData();
        }

        private void Start()
        {
            activeUnitsList = UnitManager.Instance.GetAllUnits();

            foreach (IUnit unit in activeUnitsList)
            {
                CreateUnitEntry(unit);
            }
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
        public void CreateUnitEntry(IUnit unit)
        {
            UnitEntry unitEntryData = UnitEntry.CreateInstance(unit);

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
