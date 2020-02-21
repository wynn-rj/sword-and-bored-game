using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    public interface IArmor : IEquipment
    {
        public int Physical_Defense { get; set; }
        public int Magic_Defense { get; set; }
        public IModifierDefense ModifierDefense { get; set; }
    }
}
