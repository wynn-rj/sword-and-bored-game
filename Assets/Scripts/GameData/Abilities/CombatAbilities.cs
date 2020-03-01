using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.StatusConditions;

namespace SwordAndBored.GameData.Abilities
{
    public class CombatAbilities : IAbility
    {
        public int ID { get; set; }
        public int Damage { get; set; }
        public int Accuracy { get; set; }
        public int Range { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Shape { get; set; }
        public bool IsPhysical { get; set; }
        public IStatusConditionsAttack StatusConditionsAttack { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FlavorText { get; set; }

        public enum ShapeEnum {Point = 0, Sphere = 1, Cross = 2, Line = 3}

        public CombatAbilities(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Abilities", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Name = reader.GetStringFromCol("Name");
                Description = reader.GetStringFromCol("Description");
                FlavorText = reader.GetStringFromCol("Flavor_Text");

                int statusConditionsAttackID = reader.GetIntFromCol("Status_Conditions_Attack_FK");
                StatusConditionsAttack = new StatusConditionsAttack(statusConditionsAttackID);

                Damage = reader.GetIntFromCol("Damage");
                Accuracy = reader.GetIntFromCol("Accuracy");
                Range = reader.GetIntFromCol("Range");
                IsPhysical = reader.GetIntFromCol("Is_Physical") == 1;
                Length = reader.GetIntFromCol("Length");
                Width = reader.GetIntFromCol("Width");
                Shape = reader.GetIntFromCol("Shape");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        override public string ToString()
        {
            return "{Ability: " + ID + ", Descriptor: " + Name + ", Damage: " + Damage
                + ", Accuracy: " + Accuracy + ", Range: " + Range + ", AttackModifiers: " + StatusConditionsAttack + "}";
        }
    }
}