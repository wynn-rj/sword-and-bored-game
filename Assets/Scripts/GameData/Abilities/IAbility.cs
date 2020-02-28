using SwordAndBored.GameData.StatusConditions;

namespace SwordAndBored.GameData.Abilities
{
    public interface IAbility : IDescriptable, IDatabaseObject
    {
        IStatusConditionsAttack StatusConditionsAttack { get; set; }
        int Damage { get; set; }
        int Accuracy { get; set; }
        int Range { get; set; }
        bool IsPhysical { get; set; }
    }
}
