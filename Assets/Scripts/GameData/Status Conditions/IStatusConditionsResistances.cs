namespace SwordAndBored.GameData.StatusConditions
{
    public interface IStatusConditionsResistances : IDatabaseObject
    {
        int Fire_Resist { get; set; }
        int Freeze_Resist { get; set; }
        int Bleed_Resist { get; set; }
        int Stun_Resist { get; set; }
    }
}
