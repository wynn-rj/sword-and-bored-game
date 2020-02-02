using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Creatures
{
    interface IHero : ICharacter
    {
        IAbility HeroAbility { get; }
    }
}
