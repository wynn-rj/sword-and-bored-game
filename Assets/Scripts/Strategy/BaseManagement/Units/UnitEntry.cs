using SwordAndBored.GameData.Units;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class UnitEntry : ScriptableObject
    {
        public string UnitName;
        public string Role;
        public string CurrentTown;
        public string CurrentSquad;

        private IStats stats;
        private int id;

        public void Init(IUnit unit)
        {
            this.id = unit.ID;
            this.Role = unit.Role.Name;
            this.UnitName = unit.Name;

            if (!(unit.Squad is null))
            {
                this.CurrentSquad = unit.Squad.Name;
            }

            if (!(unit.Town is null))
            {
                this.CurrentTown = unit.Town.Name;
            }
        }

        public static UnitEntry CreateInstance(IUnit unit)
        {
            var data = ScriptableObject.CreateInstance<UnitEntry>();
            data.Init(unit);
            return data;
        }
    }
}
