using Mono.Data.Sqlite;
using UnityEngine;

namespace SwordAndBored.GameData.Database
{
    public class DatabaseConnection
    {
        public SqliteConnection conn;

        private readonly string SelectAllFromString = "SELECT * FROM ";
        private readonly string WhereIDEqualsString = " WHERE ID = ";

        public DatabaseConnection()
        {
            CreateDatabaseConnection();
        }
        private void CreateDatabaseConnection()
        {
            conn = new SqliteConnection("URI=file:" + Application.streamingAssetsPath + "/GameSaves/GameData.db");
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Dispose();
        }

        public DatabaseReader ExecuteQuery(string query)
        {
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = query;
            return new DatabaseReader(command.ExecuteReader());
        }

        public int ExecuteNonQuery(string query)
        {
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        public DatabaseReader QueryAllFromTable(string tableName)
        {
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = SelectAllFromString + tableName;
            return new DatabaseReader(command.ExecuteReader());
        }

        public DatabaseReader QueryRowFromTableWithID(string tableName, int ID)
        {
            SqliteCommand command = conn.CreateCommand();
            //Debug.Log(SelectAllFromString + tableName + WhereIDEqualsString + ID.ToString());
            command.CommandText = SelectAllFromString + tableName + WhereIDEqualsString + ID.ToString();
            return new DatabaseReader(command.ExecuteReader());
        }

        public DatabaseReader QueryRowFromTableWhereColNameEqualsInt(string tableName, string colName, int match)
        {
            SqliteCommand command = conn.CreateCommand();
            //Debug.Log(SelectAllFromString + tableName + WhereIDEqualsString + match.ToString());
            command.CommandText = SelectAllFromString + tableName + " WHERE " + colName + " = " + match.ToString();
            return new DatabaseReader(command.ExecuteReader());
        }

        public DatabaseReader QueryRowFromTableWhereColNameEqualsInputStr(string tableName, string colName, string match)
        {
            SqliteCommand command = conn.CreateCommand();
            //Debug.Log(SelectAllFromString + tableName + WhereIDEqualsString + match);
            command.CommandText = SelectAllFromString + tableName + " WHERE " + colName + " = '" + match + "';";
            return new DatabaseReader(command.ExecuteReader());
        }

    }
}