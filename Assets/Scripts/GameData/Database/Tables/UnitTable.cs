using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database.Tables {
    public class UnitTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        public RoleTable Role { get; set; }
        public StatsTable Stats { get; set; }

        public UnitTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            SqliteDataReader reader = conn.QueryRowFromTableWithID("Units", inputID);

            ID = inputID;
            if (reader.Read())
            {
                int descriptorID = reader.GetInt32(reader.GetOrdinal("Descriptor_FK"));
                Descriptor = new DescriptorTable(descriptorID);

                int roleID = reader.GetInt32(reader.GetOrdinal("Role_FK"));
                Role = new RoleTable(roleID);


                int statsID = reader.GetInt32(reader.GetOrdinal("Stats_FK"));
                Stats = new StatsTable(statsID);
            }
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Unit: " + ID + ", Descriptior: " + Descriptor.ToString() + ", Role: " + Role.ShortString() + ", Stats: " + Stats.ToString() + "}";
        }

        public string LongString()
        {
            return "Unit: {ID: " + ID + ", Descriptior: " + Descriptor.LongString() + ", Role: " + Role.LongString() + ", Stats: " + Stats.LongString() + "}";
        }

        public string ShortString()
        {
            return "{Unit: " + ID + ", Descriptior: " + Descriptor.Name + ", Role: " + Role.ID + ", Stats: " + Stats.ID + "}";
        }
    }
}
