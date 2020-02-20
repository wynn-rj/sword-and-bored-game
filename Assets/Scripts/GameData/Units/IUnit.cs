using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Equipment;

namespace SwordAndBored.GameData.Units
{
    interface IUnit : IDescriptable
    {
        /// <summary>
        /// A container of all stats a creature has
        /// </summary>
        IStats Stats { get; set; }

        /// <summary>
        /// An aggregate collection of all abilities a creature has access to
        /// </summary>
        IAbility[] Abilities { get; set; }

        /// <summary>
        /// An aggregate collection of all equipment on a creature
        /// </summary>
        IWeapon[] Weapon { get; set; }

        /// <summary>
        /// An aggregate collection of all modifiers applied to a creature
        /// </summary>
        IArmor[] Armor { get; set; }

        int XP { get; set; }
        int Level { get; }
    }
}