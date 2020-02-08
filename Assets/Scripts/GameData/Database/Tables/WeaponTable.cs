using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database.Tables
{
    public class WeaponTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }

        public WeaponTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            SqliteDataReader reader = conn.QueryRowFromTableWithID("Weapons", inputID);

            ID = inputID;
            if (reader.Read())
            {
                int descriptorID = reader.GetInt32(reader.GetOrdinal("Descriptor_FK"));
                Descriptor = new DescriptorTable(descriptorID);
            }
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Weapon: " + ID + ", Descriptior: " + Descriptor.ToString() + "}";
        }

        public string LongString()
        {
            return "Weapon: {ID: " + ID + ", Descriptior: " + Descriptor.LongString() + "}";
        }

        public string ShortString()
        {
            return "{Weapon: " + ID + ", Descriptior: " + Descriptor.Name + "}";
        }
    }
}