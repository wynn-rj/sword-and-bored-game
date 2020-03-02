using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class Barracks : GenericBuilding
    {
        private IList<IUnit> activeUnitsList;

        [SerializeField] private GameObject trainUnitCanvas;

        public GameObject TrainUnitCanvas
        {
            get { return trainUnitCanvas; }
        }

        void Awake()
        {
            /*
             * TO DO: Get all active units from database and save to list
             */

            /*
             * TO DO: Create entries for all units, and place in GUI display
             */
        }

        public void TrainUnit()
        {
            MainCanvas.SetActive(false);
            trainUnitCanvas.SetActive(true);
        }

        void CreateUnitEntry()
        {
            /*
             * TO DO: Construct unit entry, add to GUI display
             */
        }
    }
}
