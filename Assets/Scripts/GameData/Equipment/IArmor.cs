using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    public interface IArmor : IEquipment
    {
        int ID { get; }
        int Physical_Defense { get; set; }
        int Magic_Defense { get; set; }
        IModifierDefense ModifierDefense { get; set; }
    }
}
