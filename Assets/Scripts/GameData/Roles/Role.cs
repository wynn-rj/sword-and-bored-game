using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Units;

namespace SwordAndBored.GameData.Roles
{
    public class Role
    {
        public int ID { get; }
        public Descriptor Descriptor { get; set; }
        public Stats Stats { get; set; }

        public Role(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Roles", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new Descriptor(descriptorID);

                int statsID = reader.GetIntFromCol("BaseStats_FK");
                Stats = new Stats(statsID);
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
