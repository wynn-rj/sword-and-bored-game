namespace SwordAndBored.GameData.Abilities.Skills
{
    /// <summary>
    /// A skill tier that can only ever have one skill selected at a time
    /// </summary>
    class SingleSelectSkillTier : AbstractSkillTier
    {
        public override IAbility[] Selected
        {
            get
            {
                if (selection != -1)
                {
                    internalSelected[0] = Choices[selection];
                }
                return internalSelected;
            }
        }
        public override bool HasSelectableSkills => selection == -1;

        // -1 indicates no skill has been selected        
        private int selection = -1;
        private IAbility[] internalSelected = new IAbility[0];

        public SingleSelectSkillTier(IAbility[] choices) : base(choices) { }

        public SingleSelectSkillTier(int choiceCount) : base(choiceCount) { }

        public override void SelectSkill(int choicesIndex)
        {
            if (choicesIndex >= 0 && choicesIndex < Choices.Length)
            {
                if (selection == -1)
                {
                    internalSelected = new IAbility[1];
                }
                selection = choicesIndex;
            }
        }
    }
}
