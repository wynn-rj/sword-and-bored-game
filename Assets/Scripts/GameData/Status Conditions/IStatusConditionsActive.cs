
namespace SwordAndBored.GameData.StatusConditions
{
    public interface IStatusConditionsActive : IDatabaseObject
    {
        bool IsStunned { get; set; }
        bool IsOnFire { get; set; }
        int FireDamage { get; set; }
        bool IsPoisoned { get; set; }
        int PoisonDamage { get; set; }
        bool IsBleeding { get; set; }
        int BleedDamage { get; set; }
    }
}
