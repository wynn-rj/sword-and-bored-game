using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    interface IEquipment : IDescriptable
    {
        /// <summary>
        /// The abilities granted by this equipment
        /// </summary>
        IAbility[] Abilities { get; }

        /// <summary>
        /// The modifiers granted by this equipment
        /// </summary>
        IModifier[] Modifiers { get; }
    }
}
