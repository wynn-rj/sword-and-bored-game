using SwordAndBored.GameData.Creatures;
using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Units;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class UnitFactory : MonoBehaviour
    {
        public string UnitRole { get; set; }
        public string UnitName { get; set; }

        public GameObject NameUnitCanvas;

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
                DatabaseConnection conn = new DatabaseConnection();
                System.Random r = new System.Random();
                int rInt = r.Next(1, 30);
                DatabaseReader databaseReader = conn.ExecuteQuery($"SELECT Name FROM Random_Names WHERE ID = {rInt}");
                databaseReader.NextRow();
                string randomName = databaseReader.GetStringFromCol("Name");
                databaseReader.CloseReader();
                conn.CloseConnection();
                character.Name = randomName;
            }
            else
            {
                character.Name = UnitName;
            }
            
            UnitManager.Instance.RegisterUnit(character);
            NameUnitCanvas.gameObject.SetActive(false);
            character.Save();
        }

        public void CancelUnitTraining()
        {
            NameUnitCanvas.gameObject.SetActive(false);
        }
    }
}
