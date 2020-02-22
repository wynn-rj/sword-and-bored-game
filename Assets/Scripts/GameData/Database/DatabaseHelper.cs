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
    }
}
