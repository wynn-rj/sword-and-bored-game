using SwordAndBored.GameData.Units;

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
            unit.Name = "Default Unit";
            unit.Save();
        }
    }
}
