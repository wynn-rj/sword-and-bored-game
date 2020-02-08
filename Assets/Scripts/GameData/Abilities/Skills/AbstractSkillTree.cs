namespace SwordAndBored.GameData.Abilities.Skills
{
    abstract class AbstractSkillTree : ISkillTree
    {
        public ISkillTier[] Tiers { get; }

        public AbstractSkillTree(int tierDepth)
        {
            Tiers = new ISkillTier[tierDepth];
        }
    }
}
