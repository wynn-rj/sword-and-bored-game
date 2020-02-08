using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace SwordAndBored.GameData.Database
{
    public class DatabaseConnection
    {
        public SqliteConnection conn;

        public DatabaseConnection()
        {
            CreateDatabaseConnection();
        }
        private void CreateDatabaseConnection()
        {
            conn = new SqliteConnection("URI=file:" + Application.dataPath + "/StreamingAssets/GameData.db");
            conn.Open();
        }

        public SqliteDataReader ExecuteQuery(string query)
        {
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = query;
            return command.ExecuteReader();
        }

        public string GetQueryStringForTable(string tableName)
        {
            return "SELECT * FROM " + tableName;
        }
    }
}