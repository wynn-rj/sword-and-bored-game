namespace SwordAndBored.GameData.Abilities.Skills
{
    /// <summary>
    /// A collection of abilities to choose from, where only one or none can be selected at a time
    /// </summary>
    interface ISkillTier
    {
        IAbility[] Choices { get; }

        IAbility[] Selected { get; set; }
    }
}
