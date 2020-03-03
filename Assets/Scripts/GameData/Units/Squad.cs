﻿using System.Collections.Generic;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units
{
    public class Squad : ISquad
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<IUnit> Units
        {
            get
            {
                List<IUnit> newUnitList = new List<IUnit>();
                DatabaseConnection conn = new DatabaseConnection();
                DatabaseReader reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Squads_FK", ID);
                while (reader.NextRow())
                {
                    int dataUnitID = reader.GetIntFromCol("ID");
                    IUnit unitInSquad = new Unit(dataUnitID);
                    newUnitList.Add(unitInSquad);
                }
                return newUnitList;
            }
            set { }
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public Squad(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Squads", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");
                X = reader.GetIntFromCol("X");
                Y = reader.GetIntFromCol("Y");

                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Squads_FK", inputID);
                while (reader.NextRow())
                {
                    int dataUnitID = reader.GetIntFromCol("ID");
                    IUnit unitInSquad = new Unit(dataUnitID);
                    Units.Add(unitInSquad);
                }
            }
            conn.CloseConnection();
            reader.CloseReader();
        }

        public Squad(int inputX, int inputY)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.ExecuteQuery($"SELECT * FROM Squads WHERE X = {inputX} AND Y = {inputY};");

            X = inputX;
            Y = inputY;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");
                ID = reader.GetIntFromCol("ID");

                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Squads_FK", ID);
                while (reader.NextRow())
                {
                    int dataUnitID = reader.GetIntFromCol("ID");
                    IUnit unitInSquad = new Unit(dataUnitID);
                    Units.Add(unitInSquad);
                }
            }
            conn.CloseConnection();
            reader.CloseReader();
        }

        public Squad(string SquadName, int newX, int newY)
        {
            X = newX;
            Y = newY;
            Name = SquadName;
            ID = -1;
        }

        public Squad(string SquadName, List<IUnit> units, int newX, int newY)
        {
            X = newX;
            Y = newY;
            Name = SquadName;
            ID = -1;
            Units = units;
        }


        public static List<ISquad> GetAllSquads()
        {
            List<ISquad> result = new List<ISquad>();
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Squads");
            while (reader.NextRow())
            {
                ISquad dataSquad = new Squad(reader.GetIntFromCol("ID"));
                result.Add(dataSquad);
            }
            reader.CloseReader();
            conn.CloseConnection();

            return result;
        }

        public int AverageMovement()
        {
            if (Units.Count == 0)
            {
                return 0;
            }
            else
            {
                int moveSpeed = 0;
                foreach (IUnit unit in Units)
                {
                    moveSpeed += unit.Stats.Movement;
                }
                return moveSpeed / Units.Count;
            }
        }

        public int Save()
        {
            if (ID == -1)
            {

            } else
            {

            }
            return ID;
        }

        public int Delete()
        {
            foreach(IUnit units in Units)
            {
                units.Squad = null;
            }
            DatabaseConnection conn = new DatabaseConnection();
            int results = conn.ExecuteNonQuery($"DELETE FROM Squads WHERE ID = {ID};");
            conn.CloseConnection();
            return results;
        }
    }
}