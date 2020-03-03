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

        public static string GetNullOrIDStringFromString(string str)
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
    }
}
