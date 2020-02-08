using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
    abstract class AbstractPassiveAbility : GenericAbility, IPassiveAbility
    {
        public IModifier[] PassiveModifiers { get; protected set; }
    }
}
