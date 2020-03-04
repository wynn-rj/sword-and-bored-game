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

        private IUnit character;

        public void StageUnitForTraining()
        {
            character = new Unit(UnitRole);
            nameUnitCanvas.SetActive(true);
        }

        public void ConfirmUnitTraining()
        {
            if (UnitName.Length == 0)
            {
                string randomName = DatabaseHelper.GetRandomNameFromDatabase();
                character.Name = randomName;
            }
            else
            {
                character.Name = UnitName;
            }
            
            UnitManager.Instance.RegisterUnit(character);
            nameUnitCanvas.gameObject.SetActive(false);
            character.Save();
            barracks.CreateUnitEntry(character);
        }

        public void CancelUnitTraining()
        {
            nameUnitCanvas.gameObject.SetActive(false);
        }
    }
}
