using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
    /// <summary>
    /// An ability that has to be activated to apply a benefit to the user
    /// 
    /// Examples: A weapon attack or Hiding 
    /// </summary>
    interface IActivatedAbility : IAbility
    {

        /// <summary>
        /// A list of modifiers that are applied to the object that has this ability when it is active
        /// </summary>
        IModifier[] ActiveModifiers { get; }

        bool IsActive { get; set; }
    }
}
