using SwordAndBored.GameData.Units;
using UnityEngine;

namespace SwordAndBored.GameData.Database
{
    public static class DatabaseHelper
    {
        public static string GetNullOrIDStringFromObject(IDatabaseObject obj)
        {
            if (obj is null)
            {
                return "null";
            }
            else
            {
                return obj.ID.ToString();
            }
        }

        public static string GetNullOrStringFromString(string str)
        {
            if (str is null)
            {
                return "null";
            }
            else
            {
                return "'" + str + "'";
            }
        }

        public static void ClearAllUnitsExceptOneFromUnitTable()
        {
            DatabaseConnection conn = new DatabaseConnection();
            conn.ExecuteNonQuery("DELETE FROM Units;");
            conn.CloseConnection();
            IUnit unit = new Unit("Mage");
            unit.Name = "Grand Mage Crawfis";
            unit.Save();
        }

        public static string GetRandomNameFromDatabase()
        {
            DatabaseConnection conn = new DatabaseConnection();
            int rInt = Random.Range(1, 30);
            DatabaseReader reader = conn.ExecuteQuery($"SELECT Name From Random_Names WHERE ID = {rInt}");
            reader.NextRow();
            string randName = reader.GetStringFromCol("Name");
            conn.CloseConnection();
            reader.CloseReader();

            return randName;
        }

        public static int GetGoldAmount()
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.ExecuteQuery($"SELECT Gold From Resources WHERE ID = 1");
            reader.NextRow();
            int goldAmount = reader.GetIntFromCol("Gold");
            conn.CloseConnection();
            reader.CloseReader();

            return goldAmount;
        }

        public static void SetGoldAmount(int newAmount)
        {
            DatabaseConnection conn = new DatabaseConnection();
            conn.ExecuteNonQuery($"UPDATE Resources SET Gold = {newAmount};");
            conn.CloseConnection();
        }

        public static int GetTurnNumber()
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.ExecuteQuery($"SELECT Turn_Number From Resources WHERE ID = 1");
            reader.NextRow();
            int turnNum = reader.GetIntFromCol("Turn_Number");
            conn.CloseConnection();
            reader.CloseReader();

            return turnNum;
        }

        public static void SetTurnNumber(int turnNum)
        {
            DatabaseConnection conn = new DatabaseConnection();
            conn.ExecuteNonQuery($"UPDATE Resources SET Turn_Number = {turnNum};");
            conn.CloseConnection();
        }
    }
}
