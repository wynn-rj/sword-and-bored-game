using SwordAndBored.GameData.Modifiers;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Equipment
{
    public class Armor
    {
        public int ID { get; }
        public Descriptor Descriptor { get; set; }
        public DefenseModifiers DefenseModifiers { get; set; }
        public int Physical_Defense { get; set; }
        public int Magic_Defense { get; set; }

        public Armor(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Armor", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new Descriptor(descriptorID);

                int defenseModifierID = reader.GetIntFromCol("Defense_Modifier_FK");
                DefenseModifiers = new DefenseModifiers(defenseModifierID);

                Physical_Defense = reader.GetIntFromCol("Physical_Defense");
                Magic_Defense = reader.GetIntFromCol("Magic_Defense");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Armor: " + ID + ", Descriptor: " + Descriptor.ToString() + ", DefenseModifier: " 
                + DefenseModifiers.ToString() + ", Physical Defense: " + Physical_Defense + ", Magic_Defense: " 
                + Magic_Defense +  "}";
        }

        public string LongString()
        {
            return "Armor: {ID: " + ID + ", Descriptor: " + Descriptor.ToString() + ", DefenseModifier: "
                + DefenseModifiers.ToString() + ", Physical Defense: " + Physical_Defense + ", Magic_Defense: "
                + Magic_Defense + "}";
        }

        public string ShortString()
        {
            return "{Armor: " + ID + ", Descriptor: " + Descriptor.Name + "}";
        }
    }
}