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
        public List<IAbility> Abilities {
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

                Abilities = new List<IAbility>();
                Abilities.AddRange(Weapon.Abilities);
                Abilities.AddRange(SpellBook.Abilities);

            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        public Unit(string RoleName)
        {
            ID = -1;
            XP = 0;
            Level = 1;

            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWhereColNameEqualsInputStr("Roles", "Name", RoleName);
            if (reader.NextRow())
            {
                int roleID = reader.GetIntFromCol("ID");
                Role = new Role(roleID);

                Stats = Role.RoleStats;
                Name = "No name/Random Default";
            } else
            {
                // Default role none matched
                Role = new Role(0);

                Stats = Role.RoleStats;
            }
        }

        public int Save()
        {
            // New Entry
            if (ID == -1)
            {
                string queryString = $"INSERT INTO Units (Name, Description, Flavor_Text, XP, Level, Stats_FK, Role_FK, Weapon_FK, Armor_FK, Spell_Book_FK) VALUES" +
                    $"('{Name}', '{Description}' , '{FlavorText}', {XP}, {Level}, {Stats.ID}, {Role.ID}, {Weapon.ID}, {Armor.ID}, {SpellBook.ID})";
                DatabaseConnection conn = new DatabaseConnection();
                return conn.ExecuteNonQuery(queryString);

            } else //Update
            {

            }
            return -1;
        }

        override public string ToString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Name.ToString() + ", Role: " + Role
                + ", Stats: " + Stats.ToString();// + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
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
