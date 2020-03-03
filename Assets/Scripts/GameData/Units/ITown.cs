using System.Collections.Generic;

namespace SwordAndBored.GameData.Units
{
    public interface ITown : IDatabaseObject, IDescriptable
    {
        int X { get; set; }
        int Y { get; set; }
        List<IUnit> Units { get; set; }
    }
}
