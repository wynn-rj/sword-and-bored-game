using System.Collections.Generic;

namespace SwordAndBored.GameData.Database.Tables
{
    public class Weapon
    {
        public int ID { get; }
        public Descriptor Descriptor { get; set; }

        public List<PhysicalAbilities> PhysicalAbilities { get; set; }

        public Weapon(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Weapons", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new Descriptor(descriptorID);
            }
            reader.CloseReader();

            reader = conn.QueryRowFromTableWhereColNameEqualsInt("Physical_Abilities", "Weapon_FK", inputID);
            PhysicalAbilities = new List<PhysicalAbilities>();
            while (reader.NextRow())
            {
                int abilityID = reader.GetIntFromCol("ID");
                PhysicalAbilities.Add(new PhysicalAbilities(abilityID));
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
            foreach (PhysicalAbilities phy in PhysicalAbilities)
            {
                result += phy.ToString();
            }
            return result;
        }
    }
}