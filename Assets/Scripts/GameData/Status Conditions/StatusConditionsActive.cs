using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.StatusConditions
{
    public class StatusConditionsActive : IStatusConditionsActive
    {
        public int ID { get; set; }
        public bool IsStunned { get; set; }
        public bool IsOnFire { get; set; }
        public int FireDamage { get; set; }
        public bool IsPoisoned { get; set; }
        public int PoisonDamage { get; set; }
        public bool IsBleeding { get; set; }
        public int BleedDamage { get; set; }

        public StatusConditionsActive(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Status_Conditions_Active", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                FireDamage = reader.GetIntFromCol("Fire_Damage");
                IsOnFire = reader.GetIntFromCol("Is_On_Fire") > 0;
                PoisonDamage = reader.GetIntFromCol("Posion_Damage");
                IsPoisoned = reader.GetIntFromCol("Is_Poisoned") > 0;
                BleedDamage = reader.GetIntFromCol("Bleed_Damage");
                IsBleeding = reader.GetIntFromCol("Is_Bleeding") > 0;
                IsStunned = reader.GetIntFromCol("Is_Stunned") > 0;
            }
            reader.CloseReader();
            conn.CloseConnection();
        }
    }
}
