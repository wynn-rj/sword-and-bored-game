using System;

namespace SwordAndBored.GameData.Abilities.Skills
{
    abstract class AbstractSkillTier : ISkillTier
    {
        public virtual IAbility[] Choices { get; }        

        public virtual IAbility[] Selected { get; }

        public AbstractSkillTier(IAbility[] choices)
        {
            Choices = (IAbility[])choices.Clone();
        }

        public AbstractSkillTier(int choiceCount)
        {
            Choices = new IAbility[choiceCount];
        }

        public abstract void SelectSkill(int choicesIndex);
    }
}
