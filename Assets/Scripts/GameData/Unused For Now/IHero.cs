using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Creatures
{
    interface IHero
    {
        IAbility HeroAbility { get; }
    }
}
