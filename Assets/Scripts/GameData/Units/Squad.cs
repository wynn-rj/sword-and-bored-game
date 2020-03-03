﻿using System.Collections.Generic;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units
{
    public class Squad : ISquad
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<IUnit> Units { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public Squad(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Towns", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");
                X = reader.GetIntFromCol("X");
                Y = reader.GetIntFromCol("Y");
            }
            conn.CloseConnection();
            reader.CloseReader();
        }

        public int AverageMovement()
        {
            throw new System.NotImplementedException();
        }

        public int Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
