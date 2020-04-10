using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.StatusConditions
{
    public class StatusConditionsResistances : IStatusConditionsResistances
    {
        public int ID { get; set; }
        public int Fire_Resist { get; set; }
        public int Freeze_Resist { get; set; }
        public int Bleed_Resist { get; set; }
        public int Stun_Resist { get; set; }

        public StatusConditionsResistances(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Status_Conditions_Resistances", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Fire_Resist = reader.GetIntFromCol("Fire_Resist");
                Freeze_Resist = reader.GetIntFromCol("Freeze_Resist");
                Bleed_Resist = reader.GetIntFromCol("Bleed_Resist");
                Stun_Resist = reader.GetIntFromCol("Stun_Resist");
            } else
            {
                Fire_Resist = 0;
                Freeze_Resist = 0;
                Bleed_Resist = 0;
                Stun_Resist = 0;
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

    }
}