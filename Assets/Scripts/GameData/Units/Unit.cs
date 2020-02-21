using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units 
{
    public class Unit
    {
        public int ID { get; }
        public Descriptor Descriptor { get; set; }
        public Role Role { get; set; }
        public Stats Stats { get; set; }

        public Weapon Weapon { get; set; }

        public Armor Armor { get; set; }

        public Unit(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Units", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new Descriptor(descriptorID);

                int roleID = reader.GetIntFromCol("Role_FK");
                Role = new Role(roleID);

                int statsID = reader.GetIntFromCol("Stats_FK");
                Stats = new Stats(statsID);

                int weaponID = reader.GetIntFromCol("Weapon_FK");
                Weapon = new Weapon(weaponID);

                int armorID = reader.GetIntFromCol("Armor_FK");
                Armor = new Armor(armorID);
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Descriptor.ToString() + ", Role: " + Role.ShortString()
                + ", Stats: " + Stats.ToString() + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }

        public string LongString()
        {
            return "Unit: {ID: " + ID + ", Descriptor: " + Descriptor.LongString() + ", Role: " + Role.LongString()
                + ", Stats: " + Stats.LongString() + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }

        public string ShortString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Descriptor.Name + ", Role: " + Role.ID + ", Stats: " 
                + Stats.ID + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }
    }
}
