using SwordAndBored.GameData.Abilities;
using System.Collections.Generic;

namespace SwordAndBored.GameData.Equipment
{
    public interface IWeapon : IEquipment
    {
        List<IAbility> Abilities { get; set; }
    }
}
