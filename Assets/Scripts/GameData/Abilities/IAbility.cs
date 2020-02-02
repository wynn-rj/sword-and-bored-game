namespace SwordAndBored.GameData.Abilities
{
    interface IAbility : IDescriptable
    {
        /// <summary>
        /// The name of the image used to represent this ability
        /// </summary>
        string ImageName { get; }
    }
}
