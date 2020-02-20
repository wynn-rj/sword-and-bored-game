using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Equipment
{
    interface ISpellBook : IEquipment
    {
        public IAbility[] Abilities { get; set; }
    }
}
