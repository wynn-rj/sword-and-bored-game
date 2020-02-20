/*using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Roles;
using System;

namespace SwordAndBored.GameData.Creatures
{
    class GenericHero : GenericCharacter, IHero
    {
        public IAbility HeroAbility { get; }

        public GenericHero(Func<int, int> xpThresholdFunction, IRole role, IAbility heroAbility, IStats stats = null) :
            base(xpThresholdFunction, role, stats)
        {
            HeroAbility = heroAbility ?? AbilityFactory.BuildAbility("Generic Hero Ability");
        }       
    }
}
*/