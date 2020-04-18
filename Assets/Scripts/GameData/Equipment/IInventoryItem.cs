using SwordAndBored.GameData.StatusConditions;

namespace SwordAndBored.GameData.Equipment
{
    public interface IInventoryItem : IDatabaseObject
    {
        IWeapon Weapon { get; }
        IArmor Armor { get; }
        ISpellBook SpellBook { get; }
        int Quantity { get; }

        void SetQuantity(int quantity);
    }
}
