using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.StatusConditions;

namespace SwordAndBored.GameData.Units
{
    public interface IUnit : IDescriptable, IDatabaseObject
    {
        /// <summary>
        /// A container of all stats a creature has
        /// </summary>
        IStats Stats { get; set; }

        /// <summary>
        /// An aggregate collection of all abilities a creature has access to
        /// </summary>
        List<IAbility> Abilities { get; set; }

        /// <summary>
        /// An aggregate collection of all equipment on a creature
        /// </summary>
        IWeapon Weapon { get; set; }

        ISpellBook SpellBook { get; set; }

        /// <summary>
        /// An aggregate collection of all modifiers applied to a creature
        /// </summary>
        IArmor Armor { get; set; }

        IRole Role { get; set; }
        IStatusConditionsActive StatusConditionsActive { get; set; }

        ISquad Squad { get; set; }
        ITown Town { get; set; }

        int XP { get; set; }
        int SquadID { get; set; }
        int Level { get; }

        int Save();
    }
}