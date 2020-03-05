using SwordAndBored.GameData.Units;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Towns
{
    public class TownEntry : ScriptableObject
    {
        public ITown town;
        public string townName;

        public void Init(ITown town)
        {
            this.town = town;
            this.townName = town.Name;
        }

        public static TownEntry CreateInstance(ITown town)
        {
            var data = ScriptableObject.CreateInstance<TownEntry>();
            data.Init(town);
            return data;
        }
    }
}