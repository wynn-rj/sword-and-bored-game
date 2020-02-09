using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database.Tables
{
    public class PhysicalAbilitiesTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        //public WeaponTable Weapon { get; set; }
        public int weaponID { get; set; }
        public AttackModifiersTable AttackModifiers { get; set; }

        public int Damage { get; set; }
        public int Accuracy { get; set; }
        public int Range { get; set; }


        public PhysicalAbilitiesTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Physical_Abilities", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new DescriptorTable(descriptorID);

                weaponID = reader.GetIntFromCol("Weapon_FK");
                //Weapon = new WeaponTable(weaponID);

                int attackModifiersID = reader.GetIntFromCol("Attack_Modifiers_FK");
                AttackModifiers = new AttackModifiersTable(attackModifiersID);

                Damage = reader.GetIntFromCol("Damage");
                Accuracy = reader.GetIntFromCol("Accuracy");
                Range = reader.GetIntFromCol("Range");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Ability: " + ID + ", Descriptior: " + Descriptor.ToString() + ", Damage: " + Damage
                + ", Accuracy: " + Accuracy + ", Range: " + Range + "}";
        }

        public string LongString()
        {
            return "Ability: {ID: " + ID + ", Descriptior: " + Descriptor.LongString() + ", Damage: " + Damage
                + ", Accuracy: " + Accuracy + ", Range: " + Range + "}";
        }

        public string ShortString()
        {
            return "{Ability: " + ID + ", Descriptior: " + Descriptor.Name + "}";
        }
    }
}