using System.Collections.Generic;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Units
{
    public class Town : ITown
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool PlayerOwned { get; set; }
        private List<IUnit> units = new List<IUnit>();
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
                conn.CloseConnection();
                reader.CloseReader();
                return newUnitList;
            }
            set { units = value; }
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
                PlayerOwned = reader.GetIntFromCol("Is_Player_Owned") > 0;

                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Towns_FK", inputID);
                while (reader.NextRow())
                {
                    int dataUnitID = reader.GetIntFromCol("ID");
                    IUnit unitInSquad = new Unit(dataUnitID);
                    units.Add(unitInSquad);
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
                PlayerOwned = reader.GetIntFromCol("Is_Player_Owned") > 0;
                ID = reader.GetIntFromCol("ID");

                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Units", "Towns_FK", ID);
                while (reader.NextRow())
                {
                    int dataUnitID = reader.GetIntFromCol("ID");
                    IUnit unitInSquad = new Unit(dataUnitID);
                    units.Add(unitInSquad);
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
            foreach (IUnit unit in units)
            {
                unit.Town = this;
                unit.Save();
            }
            int playerOwnedNum = PlayerOwned ? 1 : 0;
            string queryString = $"UPDATE Towns SET Is_Player_Owned = {playerOwnedNum} WHERE ID = {ID};";
            DatabaseConnection conn = new DatabaseConnection();
            conn.ExecuteNonQuery(queryString);
            conn.CloseConnection();
            return units.Count;
        }

    }
}
