using SwordAndBored.GameData.Modifiers;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Equipment
{
    public class Armor : IArmor
    {
        public int ID { get; set; }
        public int Physical_Defense { get; set; }
        public int Magic_Defense { get; set; }
        public IModifierDefense ModifierDefense { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public Armor(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Armor", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");

                int defenseModifierID = reader.GetIntFromCol("Defense_Modifier_FK");
                ModifierDefense = new DefenseModifiers(defenseModifierID);

                Physical_Defense = reader.GetIntFromCol("Physical_Defense");
                Magic_Defense = reader.GetIntFromCol("Magic_Defense");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Armor: " + ID + ", Descriptor: " + Name.ToString() + ", DefenseModifier: " 
                + ModifierDefense.ToString() + ", Physical Defense: " + Physical_Defense + ", Magic_Defense: " 
                + Magic_Defense +  "}";
        }

        public string LongString()
        {
            return "Armor: {ID: " + ID + ", Descriptor: " + Name.ToString() + ", DefenseModifier: "
                + ModifierDefense.ToString() + ", Physical Defense: " + Physical_Defense + ", Magic_Defense: "
                + Magic_Defense + "}";
        }

        public string ShortString()
        {
            return "{Armor: " + ID + ", Descriptor: " + Name + "}";
        }
    }
}