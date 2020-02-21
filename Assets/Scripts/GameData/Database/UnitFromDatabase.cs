using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using SwordAndBored.GameData.Database;
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
                        UnitTable unit = new UnitTable(reader.GetIntFromCol("ID"));
                        textBox.text += unit.ToString() + "\n\n";
                }
            }
        }
    }

}
