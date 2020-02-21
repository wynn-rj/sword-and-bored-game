using SwordAndBored.GameData.Abilities;
using System.Collections.Generic;

namespace SwordAndBored.GameData.Equipment
{
    public interface IWeapon : IEquipment
    {
        int ID { get; }
        List<IAbility> Abilities { get; set; }
    }
}
