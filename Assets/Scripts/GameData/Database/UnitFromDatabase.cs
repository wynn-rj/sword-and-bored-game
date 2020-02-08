using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using SwordAndBored.GameData.Database;
using TMPro;

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
            SqliteDataReader reader = conn.ExecuteQuery(conn.GetQueryStringForTable("Units"));
            while (reader.Read())
            {
                for(int i=0; i<reader.FieldCount; i++)
                {
                    textBox.text += reader.GetValue(i) + " ";
                }
                textBox.text += "\n";
            }
        }
    }
}
