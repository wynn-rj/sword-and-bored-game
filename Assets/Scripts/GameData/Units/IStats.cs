namespace SwordAndBored.GameData.Units
{
    /// <summary>
    /// A collection of stats for a creature or similar object
    /// </summary>
    public interface IStats : IDatabaseObject
    {
        int Physical_Attack { get; set; }
        int Physical_Defense { get; set; }
        int Magic_Attack { get; set; }
        int Magic_Defense { get; set; }
        int Max_HP { get; set; }
        int Current_HP { get; set; }
        int Initiative { get; set; }
        int Movement { get; set; }
        int Evasion { get; set; }
        int Accuracy { get; set; }

        int Save();
    }
}
