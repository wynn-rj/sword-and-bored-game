
namespace SwordAndBored.GameData.Database.Tables
{
    public class DescriptorTable
    {
        public int ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public DescriptorTable(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Descriptors", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Descriptor: " + ID + ", Name: " + Name + "}";
        }

        public string LongString()
        {
            return "Descriptor: {ID: " + ID + ", Name: " + Name + ", Description: " + Description + ", FlavorText: " + FlavorText + "}";
        }

        public string ShortString()
        {
            return "{Descriptor: " + ID + "}";
        }
    }
}
