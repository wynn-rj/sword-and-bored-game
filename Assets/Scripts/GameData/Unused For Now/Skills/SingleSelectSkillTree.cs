/*
 * namespace SwordAndBored.GameData.Abilities.Skills
{
    class SingleSelectSkillTree : ISkillTree
    {
        public ISkillTier[] Tiers => SingleSkillTiers;

        protected SingleSelectSkillTier[] SingleSkillTiers { get; }

        public SingleSelectSkillTree(int tierDepth)
        {
            SingleSkillTiers = new SingleSelectSkillTier[tierDepth];
        }

        public SingleSelectSkillTree(IAbility[][] abilityTiers) : this(abilityTiers.Length)
        {
            int i = 0;
            foreach (IAbility[] abilityList in abilityTiers)
            {
                SingleSkillTiers[i++] = new SingleSelectSkillTier(abilityList);
            }
        }
    }
}
*/