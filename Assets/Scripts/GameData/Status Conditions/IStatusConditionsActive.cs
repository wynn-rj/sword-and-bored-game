
namespace SwordAndBored.GameData.StatusConditions
{
    public interface IStatusConditionsActive : IDatabaseObject
    {
        bool IsStunned { get; set; }
        bool IsBurning { get; set; }
        bool IsFrozen { get; set; }
        bool IsBleeding { get; set; }
    }
}
