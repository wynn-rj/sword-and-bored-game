using System.Collections.Generic;

namespace SwordAndBored.GameData.Units
{
    public interface ISquad : IDatabaseObject, IDescriptable
    {
        int X { get; set; }
        int Y { get; set; }
        List<IUnit> Units { get; set; }

        int AverageMovement();
        int Save();
        int Delete();
    }
}
