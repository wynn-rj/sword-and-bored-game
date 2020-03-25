using SwordAndBored.GameData;
using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.GameResources;
using SwordAndBored.StrategyView.BaseManagement.Buildings;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class UnitFactory : MonoBehaviour
    {
        public string UnitRole { get; set; }
        public string UnitName { get; set; }

        public GameObject nameUnitCanvas;
        public Barracks barracks;

        [SerializeField] private ResourceManager resourceManager;

        private IUnit unit;

        private IDictionary<string, int> roleCosts = new Dictionary<string, int>()
        {
            {"Warrior", 10 },
            {"Scout", 10 },
            {"Mage", 10 },
        };

        public void StageUnitForTraining()
        {
            unit = new Unit(UnitRole);
            nameUnitCanvas.SetActive(true);
        }

        public void ConfirmUnitTraining()
        {
            if (UnitName.Length == 0)
            {
                string randomName = ResourceHelper.GetRandomNameFromDatabase();
                unit.Name = randomName;
            }
            else
            {
                unit.Name = UnitName;
            }
            
            UnitManager.Instance.RegisterUnit(unit);
            nameUnitCanvas.gameObject.SetActive(false);
            unit.Save();
            barracks.CreateUnitEntry(unit);

            IPayment trainingPayment = new Payment(unit.Role.Name, roleCosts[unit.Role.Name]);
            resourceManager.MakePayment(trainingPayment);
        }

        public void CancelUnitTraining()
        {
            nameUnitCanvas.gameObject.SetActive(false);
        }
    }
}
