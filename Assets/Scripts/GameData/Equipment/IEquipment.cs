namespace SwordAndBored.GameData.Equipment
{
    public interface IEquipment : IDescriptable, IDatabaseObject
    {
        //IModifier[] Modifiers { get; }
        int Rarity { get; set; }
    }
}
