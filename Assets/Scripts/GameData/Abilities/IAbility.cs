using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
    public interface IAbility : IDescriptable
    {
        int ID { get; }
        IModifierAttack AttackModifiers { get; set; }
        int Damage { get; set; }
        int Accuracy { get; set; }
        int Range { get; set; }
        bool IsPhysical { get; set; }
    }
}
