using SwordAndBored.GameData.Abilities;
using System.Collections.Generic;

namespace SwordAndBored.GameData.Equipment
{
    interface IWeapon : IEquipment
    {
        public List<IAbility> Abilities { get; set; }
    }
}
