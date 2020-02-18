namespace SwordAndBored.GameData.Creatures
{
    /// <summary>
    /// A collection of stats for a creature or similar object
    /// </summary>
    public interface IStats
    {
        int Health { get; set; }
        int Speed { get; set; }
        int PhysicalAttack { get; set; }
        int PhysicalDefense { get; set; }
        int MagicalAttack { get; set; }
        int MagicalDefense { get; set; }
        int Movement { get; set; }
        float BaseAccuracy { get; set; }
        float BaseEvasion { get; set; }
    }
}
