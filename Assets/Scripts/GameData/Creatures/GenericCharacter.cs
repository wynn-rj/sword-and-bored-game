using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Abilities.Skills;
using SwordAndBored.GameData.Roles;
using System;

namespace SwordAndBored.GameData.Creatures
{
    class GenericCharacter : AbstractCharacter
    {
        public override IRole Role { get; }

        public override int XP { 
            get => xp;
            set
            {
                xp = value;
                Level = xpThresholdFunction(xp);
            }
        }
        public override ISkillTree SkillTree { get; protected set; }

        public override IStats Stats { get; }

        protected int xp;
        protected Func<int, int> xpThresholdFunction = null;

        public GenericCharacter(Func<int, int>xpThresholdFunction, IRole role, IStats stats = null)
        {
            this.xpThresholdFunction = xpThresholdFunction ?? ((int x) => x);
            Role = role ?? new GenericRole(new SingleSelectSkillTree(0));
            SkillTree = role.RoleSkillTree;
            Stats = stats ?? new GenericStats();
        }

        public IHero PromoteToHero(IAbility heroAbility)
        {
            IHero hero = new GenericHero(xpThresholdFunction, Role, heroAbility, Stats);
            GenericCharacter heroAsCharacter = hero as GenericCharacter;
            heroAsCharacter.SkillTree = SkillTree;
            heroAsCharacter.internalAbilities = internalAbilities;
            heroAsCharacter.internalModifiers = internalModifiers;
            hero.XP = XP;
            hero.Name = Name;
            hero.Description = Description;
            hero.WeaponSlot = WeaponSlot;
            hero.ArmorSlot = ArmorSlot;
            hero.AdditionalEquipmentSlots = AdditionalEquipmentSlots;
            hero.ModelName = ModelName;

            return hero;
        }
    }
}
