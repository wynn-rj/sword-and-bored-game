using TMPro;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Cities
{
    public class CityEntryDisplay : MonoBehaviour
    {
        public CityEntry cityEntry;
        public TMP_Text cityName;

        public void SetDisplay()
        {
            cityName.text = cityEntry.cityName;
        }
    }
}
