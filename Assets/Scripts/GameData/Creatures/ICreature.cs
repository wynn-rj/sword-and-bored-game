using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Creatures
{
    interface ICreature : IDescriptable, IModelable
    {
        /// <summary>
        /// A container of all stats a creature has
        /// </summary>
        IStats Stats { get; }

        /// <summary>
        /// An aggregate collection of all abilities a creature has access to
        /// </summary>
        IAbility[] Abilities { get; }

        /// <summary>
        /// An aggregate collection of all equipment on a creature
        /// </summary>
        IEquipment[] Equipment { get; }

        /// <summary>
        /// An aggregate collection of all modifiers applied to a creature
        /// </summary>
        IModifier[] Modifiers { get; }
    }
}