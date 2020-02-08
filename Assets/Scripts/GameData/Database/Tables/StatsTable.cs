using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database.Tables
{
    public class StatsTable
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

        public StatsTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            SqliteDataReader reader = conn.QueryRowFromTableWithID("Stats", inputID);

            ID = inputID;
            if (reader.Read())
            {
                Physical_Attack = reader.GetInt32(reader.GetOrdinal("Physical_Attack"));
                Physical_Defense = reader.GetInt32(reader.GetOrdinal("Physical_Defense"));
                Magic_Attack = reader.GetInt32(reader.GetOrdinal("Magic_Attack"));
                Magic_Defense = reader.GetInt32(reader.GetOrdinal("Magic_Defense"));
                HP = reader.GetInt32(reader.GetOrdinal("HP"));
                Initiative = reader.GetInt32(reader.GetOrdinal("Initiative"));
                Movement = reader.GetInt32(reader.GetOrdinal("Movement"));
                Evasion = reader.GetInt32(reader.GetOrdinal("Evasion"));
                Accuracy = reader.GetInt32(reader.GetOrdinal("Accuracy"));
            }
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "Stats: {ID: " + ID + ", Physical_Attack: " + Physical_Attack + ", Physical_Defense: " + Physical_Defense + ", Magic_Attack: " + Magic_Attack 
                + ", Magic_Defense: " + Magic_Defense + ", HP: " + HP + ", Initiative: " + Initiative 
                + ", Movement: " + Movement + ", Evasion: " + Evasion + ", Accuracy: " + Accuracy + "}";
        }
    }
}
