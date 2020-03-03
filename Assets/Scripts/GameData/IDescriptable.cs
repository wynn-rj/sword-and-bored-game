namespace SwordAndBored.GameData
{
    /// <summary>
    /// An object that has a name and a description
    /// </summary>
    public interface IDescriptable
    {
        string Name { get; set; }
        string Description { get; set; }
        string FlavorText { get; set; }
    }
}