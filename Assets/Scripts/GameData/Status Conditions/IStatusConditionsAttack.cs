﻿namespace SwordAndBored.GameData.StatusConditions
{
    public interface IStatusConditionsAttack : IDatabaseObject
    {
        int Fire_Damage { get; set; }
        int Fire_Chance { get; set; }
        int Posion_Damage { get; set; }
        int Posion_Chance { get; set; }
        int Bleed_Damage { get; set; }
        int Bleed_Chance { get; set; }
        int Stun_Chance { get; set; }
    }
}
