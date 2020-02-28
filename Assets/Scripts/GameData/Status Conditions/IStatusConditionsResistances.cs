namespace SwordAndBored.GameData.Modifiers
{
    public interface IStatusConditionsResistances : IDatabaseObject
    {
        int Fire_Resist { get; set; }
        int Posion_Resist { get; set; }
        int Bleed_Resist { get; set; }
        int Stun_Resist { get; set; }
    }
}
