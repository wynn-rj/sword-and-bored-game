using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    interface IWeapon : IEquipment
    {
        public IAbility[] Abilities { get; set; }
    }
}
