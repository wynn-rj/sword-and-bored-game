﻿using System.Collections.Generic;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units
{
    public class Town : ITown
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<IUnit> Units
        {
            get
            {
                List<IUnit> newUnitList = new List<IUnit>();
                DatabaseConnection conn = new DatabaseConnection();
                DatabaseReader reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Towns_FK", ID);
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

        public Town(int inputID)
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

                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Towns_FK", inputID);
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

        public Town(int inputX, int inputY)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.ExecuteQuery($"SELECT * FROM Towns WHERE X = {inputX} AND Y = {inputY};");

            X = inputX;
            Y = inputY;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");
                ID = reader.GetIntFromCol("ID");

                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Towns_FK", ID);
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

        public static List<ITown> GetAllTowns()
        {
            List<ITown> result = new List<ITown>();
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Towns");
            while (reader.NextRow())
            {
                ITown dataSquad = new Town(reader.GetIntFromCol("ID"));
                result.Add(dataSquad);
            }
            reader.CloseReader();
            conn.CloseConnection();

            return result;
        }

        public int Save()
        {
            foreach (IUnit unit in Units)
            {
                unit.Save();
            }
            return Units.Count;
        }

    }
}