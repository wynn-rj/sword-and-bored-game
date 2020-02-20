namespace SwordAndBored.GameData.Creatures
{
    /// <summary>
    /// A collection of stats for a creature or similar object
    /// </summary>
    public interface IStats
    {
        public int Physical_Attack { get; set; }
        public int Physical_Defense { get; set; }
        public int Magic_Attack { get; set; }
        public int Magic_Defense { get; set; }
        public int HP { get; set; }
        public int Initiative { get; set; }
        public int Movement { get; set; }
        public int Evasion { get; set; }
        public int Accuracy { get; set; }
    }
}
