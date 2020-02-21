using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
    public class CombatAbilities
    {
        public int ID { get; }
        public Descriptor Descriptor { get; set; }
        //public WeaponTable Weapon { get; set; }
        public int weaponID { get; set; }
        public AttackModifiers AttackModifiers { get; set; }

        public int Damage { get; set; }
        public int Accuracy { get; set; }
        public int Range { get; set; }
        public bool IsPhysical { get; set; }


        public CombatAbilities(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Physical_Abilities", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new Descriptor(descriptorID);

                weaponID = reader.GetIntFromCol("Weapon_FK");
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
            return "{Ability: " + ID + ", Descriptor: " + Descriptor.ToString() + ", Damage: " + Damage
                + ", Accuracy: " + Accuracy + ", Range: " + Range + ", AttackModifiers: " + AttackModifiers + "}";
        }

        public string LongString()
        {
            return "Ability: {ID: " + ID + ", Descriptor: " + Descriptor.LongString() + ", Damage: " + Damage
                + ", Accuracy: " + Accuracy + ", Range: " + Range + ", AttackModifiers: " + AttackModifiers + "}";
        }

        public string ShortString()
        {
            return "{Ability: " + ID + ", Descriptor: " + Descriptor.Name + "}";
        }
    }
}