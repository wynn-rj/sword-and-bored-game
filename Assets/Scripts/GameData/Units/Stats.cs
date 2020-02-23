﻿using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units
{
    public class Stats : IStats
    {
        public int ID { get; set; }
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

        public int Save()
        {
            // New Entry
            if (ID == -1)
            {
                string queryString = "INSERT INTO Stats (Physical_Attack, Physical_Defense, Magic_Attack, Magic_Defense, HP, Initiative, Movement, Evasion" +
                    ", Accuracy) VALUES ";
                queryString += "( " + Physical_Attack + ", " + Physical_Defense + ", " + Magic_Attack + ", " + Magic_Defense + ", " + HP + ", " +
                    Initiative + ", " + Movement + ", " + Evasion + ", " + Accuracy + ");";
                DatabaseConnection conn = new DatabaseConnection();
                conn.ExecuteNonQuery(queryString);
                
                //Sets new ID equal to new entry
                DatabaseReader reader = conn.ExecuteQuery("SELECT * FROM Stats ORDER BY ID Desc LIMIT 1;");
                reader.NextRow();
                ID = reader.GetIntFromCol("ID");
                reader.CloseReader();
                conn.CloseConnection();
                return ID;
            }
            else //Update
            {
                string queryString = $"UPDATE Stats SET Physical_Attack = {Physical_Attack}, Physical_Defense = {Physical_Defense}, Magic_Attack = {Magic_Attack}" +
                    $", Magic_Defense = {Magic_Defense}, HP = {HP}, Initiative = {Initiative}, Movement = {Movement}, Evasion = {Evasion}, Accuracy = {Accuracy}" +
                    $" WHERE ID = {ID};";
                DatabaseConnection conn = new DatabaseConnection();
                return conn.ExecuteNonQuery(queryString);
            }
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
