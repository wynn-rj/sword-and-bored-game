using System;

namespace SwordAndBored.GameData.Abilities.Skills
{
    abstract class AbstractSkillTier : ISkillTier
    {
        public virtual IAbility[] Choices { get; }        

        public virtual IAbility[] Selected { get; }

        public virtual bool HasSelectableSkills => Choices.Length == Selected.Length;

        protected AbstractSkillTier(IAbility[] choices)
        {
            Choices = (IAbility[])choices.Clone();
        }

        protected AbstractSkillTier(int choiceCount)
        {
            Choices = new IAbility[choiceCount];
        }

        public abstract void SelectSkill(int choicesIndex);
    }
}
