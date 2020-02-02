using SwordAndBored.GameData.Abilities.Skills;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Roles;

namespace SwordAndBored.GameData.Creatures
{
    /// <summary>
    /// A unit that has specific equipment slots and a progressing skill tree
    /// </summary>
    interface ICharacter : ICreature
    {
        IRole Role { get; }
        int XP { get; set; }
        int Level { get; }
        ISkillTree SkillTree { get; }
        IEquipment ArmorSlot { get; set; }
        IEquipment WeaponSlot { get; set; }
        IEquipment AdditionalEquipmentSlot { get; set; }
    }
}
