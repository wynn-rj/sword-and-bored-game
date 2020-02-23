using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;
using SwordAndBored.GameData.Equipment;

namespace SwordAndBored.GameData.Units
{
    public interface IEnemy : IDescriptable, IDatabaseObject
    {
        IStats Stats { get; set; }
        List<IAbility> Abilities { get; set; }
        IWeapon Weapon { get; set; }
        ISpellBook SpellBook { get; set; }
        IArmor Armor { get; set; }

        int Tier { get; set; }
        string PreferredAI { get; set; }

    }
}