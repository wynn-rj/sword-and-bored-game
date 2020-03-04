using UnityEngine;
using UnityEngine.UI;

public class CityEntryDisplay : MonoBehaviour
{
    public CityEntry cityEntry;
    public Text cityName;

    void Start()
    {
        cityName.text = cityEntry.name;
    }
}
