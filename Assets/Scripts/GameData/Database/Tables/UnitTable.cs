
namespace SwordAndBored.GameData.Database.Tables {
    public class UnitTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        public RoleTable Role { get; set; }
        public StatsTable Stats { get; set; }

        public WeaponTable Weapon { get; set; }

        public ArmorTable Armor { get; set; }

        public UnitTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Units", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new DescriptorTable(descriptorID);

                int roleID = reader.GetIntFromCol("Role_FK");
                Role = new RoleTable(roleID);

                int statsID = reader.GetIntFromCol("Stats_FK");
                Stats = new StatsTable(statsID);

                int weaponID = reader.GetIntFromCol("Weapon_FK");
                Weapon = new WeaponTable(weaponID);

                int armorID = reader.GetIntFromCol("Armor_FK");
                Armor = new ArmorTable(armorID);
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
