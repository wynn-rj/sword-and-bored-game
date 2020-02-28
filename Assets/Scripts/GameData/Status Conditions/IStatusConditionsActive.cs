
namespace SwordAndBored.GameData.StatusConditions
{
    interface IStatusConditionsActive : IDatabaseObject
    {
        public bool IsStunned { get; set; }
        public bool IsOnFire { get; set; }
        public int FireDamage { get; set; }
        public bool IsPoisoned { get; set; }
        public int PoisonDamage { get; set; }
        public bool IsBleeding { get; set; }
        public int BleedDamage { get; set; }
    }
}
