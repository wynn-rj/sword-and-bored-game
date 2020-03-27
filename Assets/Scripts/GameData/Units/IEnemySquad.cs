using System.Collections.Generic;

namespace SwordAndBored.GameData.Units
{
    public interface IEnemySquad : IDatabaseObject
    {
        int X { get; set; }
        int Y { get; set; }
        int Save();
    }
}
