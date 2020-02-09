
namespace SwordAndBored.GameData.Database.Tables
{
    public class ArmorTable
    {
        public int ID { get; }
        public DescriptorTable Descriptor { get; set; }
        public DefenseModifiersTable DefenseModifiers { get; set; }
        public int Physical_Defense { get; set; }
        public int Magic_Defense { get; set; }

        public ArmorTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Armor", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                int descriptorID = reader.GetIntFromCol("Descriptor_FK");
                Descriptor = new DescriptorTable(descriptorID);

                int defenseModifierID = reader.GetIntFromCol("Defense_Modifier_FK");
                DefenseModifiers = new DefenseModifiersTable(defenseModifierID);

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