using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Equipment
{
    public interface IArmor : IEquipment
    {
        int Physical_Defense { get; set; }
        int Magic_Defense { get; set; }
        IStatusConditionsResistances StatusConditionsResistances { get; set; }
    }
}
