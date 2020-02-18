using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
<<<<<<< HEAD
    public interface IAbility : IDescriptable, IDatabaseObject
    {
        IModifierAttack AttackModifiers { get; set; }
        int Damage { get; set; }
        int Accuracy { get; set; }
        int Range { get; set; }
        bool IsPhysical { get; set; }
=======
    public interface IAbility : IDescriptable, IImageRepresentable
    {        
>>>>>>> Make interfaces public
    }
}
