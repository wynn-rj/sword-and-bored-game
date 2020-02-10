using System.Collections.Generic;

namespace SwordAndBored.GameData.Database.Tables
{
    public class WeaponTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }

        public List<PhysicalAbilitiesTable> PhysicalAbilities { get; set; }

        public WeaponTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Weapons", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new DescriptorTable(descriptorID);
            }
            reader.CloseReader();

            reader = conn.QueryRowFromTableWhereColNameEqualsInt("Physical_Abilities", "Weapon_FK", inputID);
            PhysicalAbilities = new List<PhysicalAbilitiesTable>();
            while (reader.NextRow())
            {
                int abilityID = reader.GetIntFromCol("ID");
                PhysicalAbilities.Add(new PhysicalAbilitiesTable(abilityID));
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Weapon: " + ID + ", Descriptor: " + Descriptor.ToString() + ", Abilities " + 
                StringAbilities() + "}";
        }

        public string LongString()
        {
            return "Weapon: {ID: " + ID + ", Descriptor: " + Descriptor.LongString() + ", Abilities " +
                StringAbilities() + "}";
        }

        public string ShortString()
        {
            return "{Weapon: " + ID + ", Descriptor: " + Descriptor.Name + "}";
        }

        private string StringAbilities()
        {
            string result = "";
            foreach (PhysicalAbilitiesTable phy in PhysicalAbilities)
            {
                result += phy.ToString();
            }
            return result;
        }
    }
}