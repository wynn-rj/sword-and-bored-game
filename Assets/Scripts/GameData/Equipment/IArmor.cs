using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    interface IArmor : IEquipment
    {
        public int Physical_Defense { get; set; }
        public int Magic_Defense { get; set; }
        public IModifierDefense ModifierDefense { get; set; }
    }
}
