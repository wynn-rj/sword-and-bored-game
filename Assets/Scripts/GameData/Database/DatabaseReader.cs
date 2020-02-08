using Mono.Data.Sqlite;

namespace SwordAndBored.GameData.Database
{
    public class DatabaseReader
    {
        public SqliteDataReader reader;

        public DatabaseReader(SqliteDataReader inputReader)
        {
            reader = inputReader;
        }

        public int GetIntFromCol(string colName)
        {
            int colNum = reader.GetOrdinal(colName);
            if (colNum < 0)
            {
                return -2;
            } else if(reader.IsDBNull(colNum))
            {
                return -1;
            } else
            {
                return reader.GetInt32(colNum);
            }
        }

        public string GetStringFromCol(string colName)
        {
            int colNum = reader.GetOrdinal(colName);
            if (colNum < 0)
            {
                return "ERROR: COLUMN DOES NOT EXIST";
            }
            else if (reader.IsDBNull(colNum))
            {
                return null;
            }
            else
            {
                return reader.GetString(colNum);
            }
        }

        public void CloseReader()
        {
            reader.Close();
        }

        public bool NextRow()
        {
            return reader.Read();
        }
    }
}