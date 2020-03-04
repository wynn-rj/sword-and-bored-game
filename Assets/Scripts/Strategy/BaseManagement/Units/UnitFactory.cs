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

        public GameObject NameUnitCanvas;
        public Barracks Barracks;

        private IUnit character;

        public void StageUnitForTraining()
        {
            character = new Unit(UnitRole);
            NameUnitCanvas.SetActive(true);
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
            NameUnitCanvas.gameObject.SetActive(false);
            character.Save();
            Barracks.CreateUnitEntry(character);
        }

        public void CancelUnitTraining()
        {
            NameUnitCanvas.gameObject.SetActive(false);
        }
    }
}
