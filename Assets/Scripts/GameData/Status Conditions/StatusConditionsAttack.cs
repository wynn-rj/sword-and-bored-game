using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.StatusConditions
{
    public class StatusConditionsAttack : IStatusConditionsAttack
    {
        public int ID { get; set; }
        public int Fire_Chance { get; set; }
        public int Freeze_Chance { get; set; }
        public int Bleed_Chance { get; set; }
        public int Stun_Chance { get; set; }

        public StatusConditionsAttack(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Status_Conditions_Attacks", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Fire_Chance = reader.GetIntFromCol("Fire_Chance");
                Freeze_Chance = reader.GetIntFromCol("Freeze_Chance");
                Bleed_Chance = reader.GetIntFromCol("Bleed_Chance");
                Stun_Chance = reader.GetIntFromCol("Stun_Chance");
            } else
            {
                Fire_Chance = 0;
                Freeze_Chance = 0;
                Bleed_Chance = 0;
                Stun_Chance = 0;
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

    }
}
