/*
using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Modifiers;
using System.Collections.Generic;

namespace SwordAndBored.GameData.Creatures
{
    abstract class AbstractCreature : ICreature
    {
        public abstract IEquipment[] Equipment { get; }
        public abstract IStats Stats { get; }

        public virtual IAbility[] Abilities
        {
            get
            {
                if (recalculateAbiltiies)
                {
                    abilities = GetAbilities();
                    recalculateAbiltiies = false;
                }
                return abilities;
            }
        }

        public virtual IModifier[] Modifiers
        {
            get
            {
                if (recalculateModifiers)
                {
                    modifiers = GetModifiers();
                    recalculateModifiers = false;
                }
                return modifiers;
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ModelName { get; set; }

        protected bool recalculateAbiltiies = true;
        protected bool recalculateModifiers = true;

        protected IAbility[] internalAbilities = null;
        protected IModifier[] internalModifiers = null;

        private IAbility[] abilities;
        private IModifier[] modifiers;

        protected virtual IModifier[] GetModifiers()
        {
            List<IModifier> modifiers = new List<IModifier>(AbilityHelper.PullModifiersFromAbilities(Abilities));
            if (internalModifiers != null)
            {
                modifiers.AddRange(internalModifiers);
            }
            foreach (IEquipment equipment in Equipment)
            {
                modifiers.AddRange(equipment.Modifiers);
            }
            return modifiers.ToArray();
        }

        protected virtual IAbility[] GetAbilities()
        {
            List<IAbility> abilities = new List<IAbility>();
            if (internalAbilities != null)
            {
                abilities.AddRange(internalAbilities);
            }
            foreach (IEquipment equipment in Equipment)
            {
                abilities.AddRange(equipment.Abilities);
            }
            return abilities.ToArray();
        }
    }
}
*/