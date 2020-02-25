using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Database;
using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Units 
{
    public class Unit : IUnit
    {
        public int ID {get; set;}
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
                Role = new Role(1);

                Stats = Role.RoleStats;
                Stats.ID = -1;
                Name = "No name/Random Default/Wrong Role";
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        public bool Save()
        {
            // New Entry
            if (ID == -1)
            {
                Stats.Save();

                string queryString = $"INSERT INTO Units (Name, Description, Flavor_Text, XP, Level, Stats_FK, Role_FK, Weapon_FK, Armor_FK, Spell_Book_FK) VALUES" +
                    $"({DatabaseHelper.GetNullOrIDStringFromString(Name)}, {DatabaseHelper.GetNullOrIDStringFromString(Description)} , {DatabaseHelper.GetNullOrIDStringFromString(FlavorText)}" +
                    $", {XP}, {Level}, {DatabaseHelper.GetNullOrIDStringFromObject(Stats)}, {DatabaseHelper.GetNullOrIDStringFromObject(Role)}, {DatabaseHelper.GetNullOrIDStringFromObject(Weapon)}," +
                    $" {DatabaseHelper.GetNullOrIDStringFromObject(Armor)}, {DatabaseHelper.GetNullOrIDStringFromObject(SpellBook)})";
                DatabaseConnection conn = new DatabaseConnection();
                conn.ExecuteNonQuery(queryString);
                DatabaseReader reader = conn.ExecuteQuery("SELECT * FROM Units ORDER BY ID Desc LIMIT 1;");
                reader.NextRow();
                ID = reader.GetIntFromCol("ID");
                reader.CloseReader();
                conn.CloseConnection();
                
                return true;

            } else //Update
            {
                Stats.Save();
                string queryString = $"UPDATE Units SET Name = {DatabaseHelper.GetNullOrIDStringFromString(Name)}, Description = {DatabaseHelper.GetNullOrIDStringFromString(Description)}, Flavor_Text = {DatabaseHelper.GetNullOrIDStringFromString(FlavorText)}," +
                    $" XP = {XP}, Level = {Level}, Stats_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Stats)}, Role_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Role)}, Weapon_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Weapon)}" +
                    $", Armor_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Armor)}, Spell_Book_FK = {DatabaseHelper.GetNullOrIDStringFromObject(SpellBook)} WHERE ID = {ID};";
                DatabaseConnection conn = new DatabaseConnection();
                conn.ExecuteNonQuery(queryString);
                conn.CloseConnection();
                return true;
            }
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
