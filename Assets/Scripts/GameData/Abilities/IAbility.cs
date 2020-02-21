using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
    public interface IAbility : IDescriptable
    {
        public IModifierAttack AttackModifiers { get; set; }
        public int Damage { get; set; }
        public int Accuracy { get; set; }
        public int Range { get; set; }
        public bool IsPhysical { get; set; }
    }
}
