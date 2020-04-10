namespace SwordAndBored.GameData.StatusConditions
{
    public interface IStatusConditionsAttack : IDatabaseObject
    {
        int Fire_Chance { get; set; }
        int Freeze_Chance { get; set; }
        int Bleed_Chance { get; set; }
        int Stun_Chance { get; set; }
    }
}
