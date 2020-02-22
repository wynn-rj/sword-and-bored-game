using UnityEngine;
using SwordAndBored.GameData.Units;
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
            if (Input.GetKeyDown(KeyCode.N)) {
                IUnit unit = new Unit("Knight");
                unit.Name = "Insert";
                unit.Save();
            }
        }
    }

}
