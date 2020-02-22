using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Units;

namespace SwordAndBored.GameData.Roles
{
    public class Role : IRole
    {
        public int ID { get; set; }
        public IStats RoleStats { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public Role(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Roles", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");

                int statsID = reader.GetIntFromCol("Base_Stats_FK");
                RoleStats = new Stats(statsID);
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Role: " + ID + ", Descriptior: " + Name.ToString() + ", Stats: " + RoleStats.ToString() + "}";
        }

        public string LongString()
        {
            return "Role: {ID: " + ID + ", Descriptior: " + Name + ", Stats: " + RoleStats + "}";
        }

        public string ShortString()
        {
            return "{Role: " + ID + ", Descriptior: " + Name + ", Stats: " + RoleStats.ID + "}";
        }
    }
}
