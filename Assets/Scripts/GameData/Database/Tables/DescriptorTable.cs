using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database.Tables
{
    public class DescriptorTable
    {
        public int ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public DescriptorTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            SqliteDataReader reader = conn.QueryRowFromTableWithID("Descriptors", inputID);

            ID = inputID;
            if (reader.Read())
            {
                Name = reader.GetString(reader.GetOrdinal("Name"));
                Description = reader.GetString(reader.GetOrdinal("Description"));
                FlavorText = reader.GetString(reader.GetOrdinal("Flavor_Text"));
            }
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "Descriptor: {ID: " + ID + ", Name: " + Name + ", Description: " + Description + ", FlavorText: " + FlavorText + "}";
        }
    }
}
