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
            DatabaseReader reader = conn.QueryRowFromTableWithID("Roles", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new DescriptorTable(descriptorID);

                int statsID = reader.GetIntFromCol("BaseStats_FK");
                Stats = new StatsTable(statsID);
            }
            reader.CloseReader();
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
