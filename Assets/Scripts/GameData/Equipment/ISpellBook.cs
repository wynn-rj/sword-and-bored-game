using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    interface ISpellBook : IEquipment
    {
        public IAbility[] Abilities { get; set; }
    }
}
