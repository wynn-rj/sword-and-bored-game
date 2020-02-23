using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Database;
using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Units
{
    public class Enemy : IEnemy
    {
        public int ID { get; set; }
        public ISpellBook SpellBook { get; set; }
        public IStats Stats { get; set; }
        public List<IAbility> Abilities
        {
            get
            {
                List<IAbility> NewAbilities = new List<IAbility>();
                NewAbilities.AddRange(Weapon.Abilities);
                NewAbilities.AddRange(SpellBook.Abilities);
                return NewAbilities;
            }
            set
            {

            }
        }
        public IWeapon Weapon { get; set; }
        public IArmor Armor { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }
        public int Tier { get; set; }
        public string PreferredAI { get; set; }

        public Enemy(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Enemies", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");

                int statsID = reader.GetIntFromCol("Stats_FK");
                Stats = new Stats(statsID);

                int weaponID = reader.GetIntFromCol("Weapon_FK");
                Weapon = new Weapon(weaponID);

                int armorID = reader.GetIntFromCol("Armor_FK");
                Armor = new Armor(armorID);

                int spellBookID = reader.GetIntFromCol("Spell_Book_FK");
                SpellBook = new SpellBook(spellBookID);

                Tier = reader.GetIntFromCol("Tier");
                PreferredAI = reader.GetStringFromCol("Preferred_AI");

                Abilities = new List<IAbility>();
                Abilities.AddRange(Weapon.Abilities);
                Abilities.AddRange(SpellBook.Abilities);

            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Name
                + ", Stats: " + Stats.ToString();// + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }

        public string LongString()
        {
            return "Unit: {ID: " + ID + ", Descriptor: " + Name 
                + ", Stats: " + Stats + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }

        public string ShortString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Name + ", Stats: "
                + Stats + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }
    }
}
