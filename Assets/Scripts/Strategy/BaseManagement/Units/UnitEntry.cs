using SwordAndBored.GameData.Units;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class UnitEntry : ScriptableObject
    {
        public string unitName;
        public string role;
        public string currentTown;
        public string currentSquad;

        private IStats stats;
        private int id;

        public void Init(IUnit unit)
        {
            this.id = unit.ID;
            this.role = unit.Role.Name;
            this.unitName = unit.Name;

            if (!(unit.Squad is null))
            {
                this.currentSquad = unit.Squad.Name;
            }

            if (!(unit.Town is null))
            {
                this.currentTown = unit.Town.Name;
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
