using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units
{
    public class Stats
    {
        public int ID { get; }
        public int Physical_Attack { get; set; }
        public int Physical_Defense { get; set; }
        public int Magic_Attack { get; set; }
        public int Magic_Defense { get; set; }
        public int HP { get; set; }
        public int Initiative { get; set; }
        public int Movement { get; set; }
        public int Evasion { get; set; }
        public int Accuracy { get; set; }

        public Stats(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Stats", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Physical_Attack = reader.GetIntFromCol("Physical_Attack");
                Physical_Defense = reader.GetIntFromCol("Physical_Defense");
                Magic_Attack = reader.GetIntFromCol("Magic_Attack");
                Magic_Defense = reader.GetIntFromCol("Magic_Defense");
                HP = reader.GetIntFromCol("HP");
                Initiative = reader.GetIntFromCol("Initiative");
                Movement = reader.GetIntFromCol("Movement");
                Evasion = reader.GetIntFromCol("Evasion");
                Accuracy = reader.GetIntFromCol("Accuracy");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Stats: " + ID + ", Physical_Attack: " + Physical_Attack + ", Physical_Defense: " + Physical_Defense + ", Magic_Attack: " + Magic_Attack 
                + ", Magic_Defense: " + Magic_Defense + ", HP: " + HP + ", Initiative: " + Initiative 
                + ", Movement: " + Movement + ", Evasion: " + Evasion + ", Accuracy: " + Accuracy + "}";
        }

        public string LongString()
        {
            return "Stats: {ID: " + ID + ", Physical_Attack: " + Physical_Attack + ", Physical_Defense: " + Physical_Defense + ", Magic_Attack: " + Magic_Attack
                + ", Magic_Defense: " + Magic_Defense + ", HP: " + HP + ", Initiative: " + Initiative
                + ", Movement: " + Movement + ", Evasion: " + Evasion + ", Accuracy: " + Accuracy + "}";
        }

        public string ShortString()
        {
            return "{Stats: " + ID + "}";
        }
    }
}
