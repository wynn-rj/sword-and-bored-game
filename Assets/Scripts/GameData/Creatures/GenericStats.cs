namespace SwordAndBored.GameData.Creatures
{
    class GenericStats : IStats
    {
        public int Health { get; set; }
        public int Speed { get; set; }
        public int PhysicalAttack { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicalAttack { get; set; }
        public int MagicalDefense { get; set; }
        public int Movement { get; set; }
        public float BaseAccuracy { get; set; }
        public float BaseEvasion { get; set; }
    }
}
