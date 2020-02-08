using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;

namespace SwordAndBored.GameData.Database { 
    public class TestConnection : MonoBehaviour
    {
        public TMP_Text text;
        // Start is called before the first frame update
        void Start()
        {
            string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/GameData.db"; //Path to database.
            SqliteConnection dbconn;
            dbconn = new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            SqliteCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT * FROM Units";
            dbcmd.CommandText = sqlQuery;
            SqliteDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                int value = reader.GetInt32(0);
                Debug.Log("value= " + value);
                text.SetText("value= " + value);
            }
            reader.Close();
            dbcmd.Dispose();
            dbconn.Close();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/GameData.db";
                Debug.Log(Application.dataPath);    //Path to database.
                IDbConnection dbconn;
                dbconn = (IDbConnection)new SqliteConnection(conn);
                dbconn.Open(); //Open connection to the database.
                text.SetText(conn);

                dbconn.Close();
            }
        }
    }
}