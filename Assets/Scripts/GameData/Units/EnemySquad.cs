using System.Collections.Generic;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units
{
    public class EnemySquad : IEnemySquad
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ID { get; set; }

        public EnemySquad(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Enemy_Squads", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                X = reader.GetIntFromCol("X");
                Y = reader.GetIntFromCol("Y");
            }
            conn.CloseConnection();
            reader.CloseReader();
        }

        public EnemySquad(int newX, int newY)
        {
            X = newX;
            Y = newY;
            ID = -1;
        }


        public static List<IEnemySquad> GetAllEnemySquads()
        {
            List<IEnemySquad> result = new List<IEnemySquad>();
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Enemy_Squads");
            while (reader.NextRow())
            {
                IEnemySquad dataSquad = new EnemySquad(reader.GetIntFromCol("ID"));
                result.Add(dataSquad);
            }
            reader.CloseReader();
            conn.CloseConnection();

            return result;
        }

        public int Save()
        {
            if (ID == -1)
            {
                string queryString = $"INSERT INTO Enemy_Squads (X, Y) VALUES ({X}, {Y});";
                DatabaseConnection conn = new DatabaseConnection();
                conn.ExecuteNonQuery(queryString);
                DatabaseReader reader = conn.ExecuteQuery("SELECT * FROM Enemy_Squads ORDER BY ID Desc LIMIT 1;");
                reader.NextRow();
                ID = reader.GetIntFromCol("ID");
                reader.CloseReader();
                conn.CloseConnection();

                return ID;
            }
            else
            {
                string queryString = $"UPDATE Enemy_Squads SET X = {X}, Y = {Y} WHERE ID = {ID};";
                DatabaseConnection conn = new DatabaseConnection();
                conn.ExecuteNonQuery(queryString);
                conn.CloseConnection();

                return ID;
            }
        }

        public int Delete()
        {
            DatabaseConnection conn = new DatabaseConnection();
            int results = conn.ExecuteNonQuery($"DELETE FROM Enemy_Squads WHERE ID = {ID};");
            conn.CloseConnection();
            return results;
        }
    }
}
