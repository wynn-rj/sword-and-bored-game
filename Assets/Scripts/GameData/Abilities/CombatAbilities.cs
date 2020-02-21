using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
    public class CombatAbilities : IAbility
    {
        public int ID { get; }
        //public WeaponTable Weapon { get; set; }
        public int weaponID { get; set; }
        public int Damage { get; set; }
        public int Accuracy { get; set; }
        public int Range { get; set; }
        public bool IsPhysical { get; set; }
        public IModifierAttack AttackModifiers { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public CombatAbilities(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Abilities", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");

                //weaponID = reader.GetIntFromCol("Weapon_FK");
                //Weapon = new WeaponTable(weaponID);

                int attackModifiersID = reader.GetIntFromCol("Attack_Modifiers_FK");
                AttackModifiers = new AttackModifiers(attackModifiersID);

                Damage = reader.GetIntFromCol("Damage");
                Accuracy = reader.GetIntFromCol("Accuracy");
                Range = reader.GetIntFromCol("Range");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Ability: " + ID + ", Descriptor: " + Name + ", Damage: " + Damage
                + ", Accuracy: " + Accuracy + ", Range: " + Range + ", AttackModifiers: " + AttackModifiers + "}";
        }

        public string LongString()
        {
            return "Ability: {ID: " + ID + ", Descriptor: " + Name + ", Damage: " + Damage
                + ", Accuracy: " + Accuracy + ", Range: " + Range + ", AttackModifiers: " + AttackModifiers + "}";
        }

        public string ShortString()
        {
            return "{Ability: " + ID + ", Descriptor: " + Name + "}";
        }
    }
}