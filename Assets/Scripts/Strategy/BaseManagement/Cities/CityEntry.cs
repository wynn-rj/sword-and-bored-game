using SwordAndBored.GameData.Units;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Cities
{
    public class CityEntry : ScriptableObject
    {
        public ITown city;

        public string cityName;

        public void Init(ITown city)
        {
            this.city = city;
            this.cityName = city.Name;
        }

        public static CityEntry CreateInstance(ITown city)
        {
            var data = ScriptableObject.CreateInstance<CityEntry>();
            data.Init(city);
            return data;
        }
    }
}
