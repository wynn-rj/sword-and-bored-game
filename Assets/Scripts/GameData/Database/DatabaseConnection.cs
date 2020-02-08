using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace SwordAndBored.GameData.Database
{
    public class DatabaseConnection
    {
        public SqliteConnection conn;

        private string SelectAllFromString = "SELECT * FROM ";
        private string WhereIDEqualsString = " WHERE ID = ";

        public DatabaseConnection()
        {
            CreateDatabaseConnection();
        }
        private void CreateDatabaseConnection()
        {
            conn = new SqliteConnection("URI=file:" + Application.dataPath + "/StreamingAssets/GameData.db");
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public SqliteDataReader ExecuteQuery(string query)
        {
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = query;
            return command.ExecuteReader();
        }

        public SqliteDataReader QueryAllFromTable(string tableName)
        {
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = SelectAllFromString + tableName;
            return command.ExecuteReader();
        }

        public SqliteDataReader QueryRowFromTableWithID(string tableName, int ID)
        {
            SqliteCommand command = conn.CreateCommand();
            Debug.Log(SelectAllFromString + tableName + WhereIDEqualsString + ID.ToString());
            command.CommandText = SelectAllFromString + tableName + WhereIDEqualsString + ID.ToString();
            return command.ExecuteReader();
        }
    }
}