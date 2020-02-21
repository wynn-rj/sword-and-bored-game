using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Modifiers
{
    public class DefenseModifiers
    {
        public int ID { get; set; }
        public int Fire_Resist { get; set; }
        public int Posion_Resist { get; set; }
        public int Bleed_Resist { get; set; }
        public int Stun_Resist { get; set; }

        public DefenseModifiers(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Defense_Modifiers", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Fire_Resist = reader.GetIntFromCol("Fire_Resist");
                Posion_Resist = reader.GetIntFromCol("Posion_Resist");
                Bleed_Resist = reader.GetIntFromCol("Bleed_Resist");
                Stun_Resist = reader.GetIntFromCol("Stun_Resist");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{DefenseModifier: " + ID + ", Fire_Resist: " + Fire_Resist + ", Posion_Resist: " + Posion_Resist
                + ", Bleed_Resist: " + Bleed_Resist + ", Stun_Resist: " + Stun_Resist + "}";
        }

        public string LongString()
        {
            return "DefenseModifier: {ID: " + ID + ", Fire_Resist: " + Fire_Resist + ", Posion_Resist: " + Posion_Resist
                + ", Bleed_Resist: " + Bleed_Resist + ", Stun_Resist: " + Stun_Resist + "}";
        }

        public string ShortString()
        {
            return "{DefenseModifier: " + ID + "}";
        }
    }
}