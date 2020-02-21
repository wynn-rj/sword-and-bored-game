/*using System.Collections.Generic;
using SwordAndBored.GameData.Modifiers;

namespace SwordAndBored.GameData.Abilities
{
    static class AbilityHelper
    {
        public static IModifier[] PullModifiersFromAbilities(IAbility[] abilities)
        {
            List<IModifier> modifiers = new List<IModifier>();

            foreach (IAbility ability in abilities)
            {
                if (ability is IActivatedAbility activatedAbility)
                {
                    if (activatedAbility.IsActive)
                    {
                        modifiers.AddRange(activatedAbility.ActiveModifiers);
                    }
                }
                if (ability is IPassiveAbility passiveAbility)
                {
                    modifiers.AddRange(passiveAbility.PassiveModifiers);
                }
            }
            return modifiers.ToArray();
        }
    }
}
*/