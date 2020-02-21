using System.Collections.Generic;
using SwordAndBored.GameData.Abilities;

namespace SwordAndBored.GameData.Equipment
{
    public interface ISpellBook : IEquipment
    {
        public List<IAbility> Abilities { get; set; }
    }
}
