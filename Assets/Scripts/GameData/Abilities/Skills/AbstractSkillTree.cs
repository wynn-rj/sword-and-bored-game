namespace SwordAndBored.GameData.Abilities.Skills
{
    abstract class AbstractSkillTree : ISkillTree
    {
        public virtual ISkillTier[] Tiers { get; }

        protected AbstractSkillTree(int tierDepth)
        {
            Tiers = new ISkillTier[tierDepth];
        }
    }
}
