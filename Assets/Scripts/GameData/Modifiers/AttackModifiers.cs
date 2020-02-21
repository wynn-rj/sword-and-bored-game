using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Modifiers
{
    public class AttackModifiers
    {
        public int ID { get; }
        public int Fire_Damage { get; set; }
        public int Fire_Chance { get; set; }
        public int Posion_Damage { get; set; }
        public int Posion_Chance { get; set; }
        public int Bleed_Damage { get; set; }
        public int Bleed_Chance { get; set; }
        public int Stun_Chance { get; set; }

        public AttackModifiers(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Attack_Modifiers", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Fire_Damage = reader.GetIntFromCol("Fire_Damage");
                Fire_Chance = reader.GetIntFromCol("Fire_Chance");
                Posion_Damage = reader.GetIntFromCol("Posion_Damage");
                Posion_Chance = reader.GetIntFromCol("Posion_Chance");
                Bleed_Damage = reader.GetIntFromCol("Bleed_Damage");
                Bleed_Chance = reader.GetIntFromCol("Bleed_Chance");
                Stun_Chance = reader.GetIntFromCol("Stun_Chance");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{AttackModifiers: " + ID + ", Fire: " + Fire_Damage + "(" + Fire_Chance + "%)" +
                ", Posion: " + Posion_Damage + "(" + Posion_Chance + "%)" +
                ", Bleed: " + Bleed_Damage + "(" + Bleed_Chance + "%)" + ", Stun: (" + Stun_Chance + "%)}";
        }

        public string ShortString()
        {
            return "{AttackModifiers: " + ID + ", Fire: " + Fire_Damage + "(" + Fire_Chance + "%)" +
                ", Posion: " + Posion_Damage + "(" + Posion_Chance + "%)" +
                ", Bleed: " + Bleed_Damage + "(" + Bleed_Chance + "%)" + ", Stun: (" + Stun_Chance + "%)}";
        }

        public string LongString()
        {
            return "{AttackModifiers: " + ID + ", Fire: " + Fire_Damage + "(" + Fire_Chance + "%)" + 
                ", Posion: " + Posion_Damage + "(" + Posion_Chance + "%)" +
                ", Bleed: " + Bleed_Damage + "(" + Bleed_Chance + "%)" + ", Stun: (" + Stun_Chance +  "%)}";
        }
    }
}
