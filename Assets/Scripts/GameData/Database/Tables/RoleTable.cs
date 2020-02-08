using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database.Tables {
    public class RoleTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        public StatsTable Stats { get; set; }

        public RoleTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            SqliteDataReader reader = conn.QueryRowFromTableWithID("Roles", inputID);

            ID = inputID;
            if (reader.Read())
            {
                int descriptorID = reader.GetInt32(reader.GetOrdinal("Descriptor_FK"));
                Descriptor = new DescriptorTable(descriptorID);

                int statsID = reader.GetInt32(reader.GetOrdinal("BaseStats_FK"));
                Stats = new StatsTable(statsID);
            }
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Role: " + ID + ", Descriptior: " + Descriptor.ToString() + ", Stats: " + Stats.ToString() + "}";
        }

        public string LongString()
        {
            return "Role: {ID: " + ID + ", Descriptior: " + Descriptor.LongString() + ", Stats: " + Stats.LongString() + "}";
        }

        public string ShortString()
        {
            return "{Role: " + ID + ", Descriptior: " + Descriptor.Name + ", Stats: " + Stats.ID + "}";
        }
    }
}
