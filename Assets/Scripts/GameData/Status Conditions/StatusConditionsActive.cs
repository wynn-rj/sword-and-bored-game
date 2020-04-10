using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.StatusConditions
{
    public class StatusConditionsActive : IStatusConditionsActive
    {
        public int ID { get; set; }
        public bool IsStunned { get; set; }
        public bool IsBurning { get; set; }
        public bool IsFrozen { get; set; }  
        public bool IsBleeding { get; set; }

        public StatusConditionsActive(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Status_Conditions_Active", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                IsBurning = reader.GetIntFromCol("Is_On_Fire") > 0;
                IsFrozen = reader.GetIntFromCol("Is_Frozen") > 0;
                IsBleeding = reader.GetIntFromCol("Is_Bleeding") > 0;
                IsStunned = reader.GetIntFromCol("Is_Stunned") > 0;
            }
            reader.CloseReader();
            conn.CloseConnection();
        }
    }
}
