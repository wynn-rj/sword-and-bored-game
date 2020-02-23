using UnityEngine;
using SwordAndBored.GameData.Units;
using SwordAndBored.GameData.Roles;
using TMPro;

namespace SwordAndBored.GameData.Database
{
    public class UnitFromDatabase : MonoBehaviour
    {
        public TMP_Text textBox;
        private DatabaseConnection conn;
        // Start is called before the first frame update
        void Start()
        {
            conn = new DatabaseConnection();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                DatabaseReader reader = conn.QueryAllFromTable("Units");
                while (reader.NextRow())
                {
                        Unit unit = new Unit(reader.GetIntFromCol("ID"));
                        textBox.text += unit.ToString() + "\n\n";
                }
                reader.CloseReader();
                conn.CloseConnection();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Unit unit = new Unit(1);
                IStats stats = unit.Stats;
                stats.HP -= 1;
                textBox.text = stats.Save().ToString();
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                textBox.text = createNewUnit().ToString(); 
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                IRole role = new Role(1);
                textBox.text = role.RoleStats.ToString();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                IEnemy enemy = new Enemy(1);
                textBox.text = enemy.ToString();
            }
        }

        private static int createNewUnit()
        {
            IUnit unit = new Unit("Knight")
            {
                Name = "Insert"
            };
            unit.Save();
            return unit.ID;
        }
    }

}
