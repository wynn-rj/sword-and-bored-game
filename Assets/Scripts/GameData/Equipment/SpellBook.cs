using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Equipment
{
    public class SpellBook : ISpellBook
    {
        public int ID { get; set; }
        public List<IAbility> Abilities { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }
        public int Rarity { get; set; }

        public SpellBook(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Spell_Books", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");
                Rarity = reader.GetIntFromCol("Rarity");
            }
            reader.CloseReader();

            reader = conn.QueryRowFromTableWhereColNameEqualsInt("Spell_Book_To_Ability", "Spell_Book_FK", inputID);
            Abilities = new List<IAbility>();
            while (reader.NextRow())
            {
                int abilityID = reader.GetIntFromCol("Abilities_FK");
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

        private string StringAbilities()
        {
            string result = "";
            foreach (IAbility ability in Abilities)
            {
                result += ability.ToString();
            }
            return result;
        }
    }
}