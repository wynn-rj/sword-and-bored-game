using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Database;
using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Units 
{
    public class Unit : IUnit
    {
        public int ID { get; }
        public ISpellBook SpellBook { get; set; }
        public IStats Stats { get; set; }
        public List<IAbility> Abilities { get; set; }
        public IWeapon Weapon { get; set; }
        public IArmor Armor { get; set; }
        public IRole Role { get; set; }
        
        public int XP { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public Unit(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Units", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");

                int roleID = reader.GetIntFromCol("Role_FK");
                Role = new Role(roleID);

                int statsID = reader.GetIntFromCol("Stats_FK");
                Stats = new Stats(statsID);

                int weaponID = reader.GetIntFromCol("Weapon_FK");
                Weapon = new Weapon(weaponID);

                int armorID = reader.GetIntFromCol("Armor_FK");
                Armor = new Armor(armorID);

                int spellBookID = reader.GetIntFromCol("Spell_Book_FK");
                SpellBook = new SpellBook(spellBookID);
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Name.ToString() + ", Role: " + Role
                + ", Stats: " + Stats.ToString() + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }

        public string LongString()
        {
            return "Unit: {ID: " + ID + ", Descriptor: " + Name + ", Role: " + Role
                + ", Stats: " + Stats + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }

        public string ShortString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Name + ", Role: " + Role.ID + ", Stats: " 
                + Stats + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }
    }
}
