using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Units;
using SwordAndBored.StrategyView.BaseManagement.Buildings;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class UnitFactory : MonoBehaviour
    {
        public string UnitRole { get; set; }
        public string UnitName { get; set; }

        public GameObject nameUnitCanvas;
        public Barracks barracks;

        private IUnit unit;

        public void StageUnitForTraining()
        {
            unit = new Unit(UnitRole);
            nameUnitCanvas.SetActive(true);
        }

        public void ConfirmUnitTraining()
        {
            if (UnitName.Length == 0)
            {
                string randomName = DatabaseHelper.GetRandomNameFromDatabase();
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
        }

        public void CancelUnitTraining()
        {
            nameUnitCanvas.gameObject.SetActive(false);
        }
    }
}
