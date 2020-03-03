using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Database;
using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.StatusConditions;

namespace SwordAndBored.GameData.Units
{
    public class Unit : IUnit
    {
        public int ID { get; set; }
        public bool IsDead { get; set; }
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
        public IRole Role { get; set; }
        public IStatusConditionsActive StatusConditionsActive { get; set; }
        public ITown Town
        {
            get
            {
                return new Town(TownID);
            }
            set { }
        }
        public ISquad Squad
        {
            get
            {
                return new Squad(SquadID);
            }
            set { }
        }
        public int XP { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        private int SquadID { get; set; }
        private int TownID { get; set; }

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

                int statusConditionActiveID = reader.GetIntFromCol("Status_Conditions_Acitve_FK");
                StatusConditionsActive = new StatusConditionsActive(statusConditionActiveID);

                SquadID = reader.GetIntFromCol("Squads_FK");
                TownID = reader.GetIntFromCol("Towns_FK");

                IsDead = reader.GetIntFromCol("Is_Dead") > 0;

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
            IsDead = false;

            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWhereColNameEqualsInputStr("Roles", "Name", RoleName);
            if (reader.NextRow())
            {
                int roleID = reader.GetIntFromCol("ID");
                Role = new Role(roleID);

                Stats = Role.RoleStats;
                Stats = new Stats(Role.RoleStats);
                Name = "No name/Random Default";
            }
            else
            {
                // Default role none matched
                Role = new Role(1);

                Stats = Role.RoleStats;
                Stats = new Stats(Role.RoleStats);
                Name = "No name/Random Default/Wrong Role";
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        public int Save()
        {
            int deadValue = IsDead ? 1 : 0;
            // New Entry
            if (ID == -1)
            {
                Stats.Save();

                string queryString = $"INSERT INTO Units (Name, Description, Flavor_Text, XP, Level, Stats_FK, Role_FK, Weapon_FK, Armor_FK, Spell_Book_FK, Squads_FK, Towns_FK, Is_Dead, " +
                    $"Status_Conditions_Active_FK) VALUES ({DatabaseHelper.GetNullOrIDStringFromString(Name)}, {DatabaseHelper.GetNullOrIDStringFromString(Description)} , " +
                    $"{DatabaseHelper.GetNullOrIDStringFromString(FlavorText)}, {XP}, {Level}, {DatabaseHelper.GetNullOrIDStringFromObject(Stats)}, {DatabaseHelper.GetNullOrIDStringFromObject(Role)}," +
                    $" {DatabaseHelper.GetNullOrIDStringFromObject(Weapon)}, {DatabaseHelper.GetNullOrIDStringFromObject(Armor)}, {DatabaseHelper.GetNullOrIDStringFromObject(SpellBook)}," +
                    $" {DatabaseHelper.GetNullOrIDStringFromObject(Squad)}, {DatabaseHelper.GetNullOrIDStringFromObject(Town)}, {deadValue}, {DatabaseHelper.GetNullOrIDStringFromObject(StatusConditionsActive)})";
                DatabaseConnection conn = new DatabaseConnection();
                conn.ExecuteNonQuery(queryString);
                DatabaseReader reader = conn.ExecuteQuery("SELECT * FROM Units ORDER BY ID Desc LIMIT 1;");
                reader.NextRow();
                ID = reader.GetIntFromCol("ID");
                reader.CloseReader();
                conn.CloseConnection();

                return ID;

            }
            else //Update
            {
                Stats.Save();
                string queryString = $"UPDATE Units SET Name = {DatabaseHelper.GetNullOrIDStringFromString(Name)}, Description = {DatabaseHelper.GetNullOrIDStringFromString(Description)}, Flavor_Text = {DatabaseHelper.GetNullOrIDStringFromString(FlavorText)}," +
                    $" XP = {XP}, Level = {Level}, Stats_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Stats)}, Role_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Role)}, Weapon_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Weapon)}" +
                    $", Armor_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Armor)}, Spell_Book_FK = {DatabaseHelper.GetNullOrIDStringFromObject(SpellBook)}, Towns_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Town)}," +
                    $" Squads_FK = {DatabaseHelper.GetNullOrIDStringFromObject(Squad)}, Status_Conditions_Active_FK = {DatabaseHelper.GetNullOrIDStringFromObject(StatusConditionsActive)} " +
                    $" Is_Dead = {deadValue} WHERE ID = {ID};";
                DatabaseConnection conn = new DatabaseConnection();
                conn.ExecuteNonQuery(queryString);
                conn.CloseConnection();
                return ID;
            }
        }

        public static List<IUnit> GetAllUnits()
        {
            List<IUnit> result = new List<IUnit>();
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Units");
            while (reader.NextRow())
            {
                IUnit dataUnit = new Unit(reader.GetIntFromCol("ID"));
                result.Add(dataUnit);
            }
            reader.CloseReader();
            conn.CloseConnection();

            return result;
        }

        override public string ToString()
        {
            return "{Unit: " + ID + ", Descriptor: " + Name.ToString() + ", Role: " + Role
                + ", Stats: " + Stats.ToString();// + ", Weapon: " + Weapon.ToString() + ", Armor: " + Armor.ToString() + "}";
        }
    }
}
