namespace SwordAndBored.GameData.Modifiers
{
    public interface IModifierAttack
    {
        public int Fire_Damage { get; set; }
        public int Fire_Chance { get; set; }
        public int Posion_Damage { get; set; }
        public int Posion_Chance { get; set; }
        public int Bleed_Damage { get; set; }
        public int Bleed_Chance { get; set; }
        public int Stun_Chance { get; set; }
    }
}
