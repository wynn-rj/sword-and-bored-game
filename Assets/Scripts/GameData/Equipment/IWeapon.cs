using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Equipment
{
    interface IWeapon : IEquipment
    {
        public IAbility[] Abilities { get; set; }
    }
}
