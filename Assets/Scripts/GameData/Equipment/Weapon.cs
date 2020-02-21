using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Equipment
{
    public class Weapon : IWeapon
    {
        public int ID { get; }
        public List<IAbility> Abilities { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public Weapon(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Weapons", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                //int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                //Descriptor = new Descriptor(descriptorID);
            }
            reader.CloseReader();

            reader = conn.QueryRowFromTableWhereColNameEqualsInt("Physical_Abilities", "Weapon_FK", inputID);
            Abilities = new List<IAbility>();
            while (reader.NextRow())
            {
                int abilityID = reader.GetIntFromCol("ID");
                Abilities.Add(new CombatAbilities(abilityID));
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Weapon: " + ID + ", Descriptor: " + Name.ToString() + ", Abilities " + 
                StringAbilities() + "}";
        }

        public string LongString()
        {
            return "Weapon: {ID: " + ID + ", Descriptor: " + Name + ", Abilities " +
                StringAbilities() + "}";
        }

        public string ShortString()
        {
            return "{Weapon: " + ID + ", Descriptor: " + Name + "}";
        }

        private string StringAbilities()
        {
            string result = "";
            foreach (CombatAbilities phy in CombatAbilities)
            {
                result += phy.ToString();
            }
            return result;
        }
    }
}