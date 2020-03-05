using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Units;
using SwordAndBored.StrategyView.BaseManagement.Buildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityDispatcher : MonoBehaviour
{
    [SerializeField] private Barracks barracks;
    [SerializeField] private DisplayDataController unitDisplayController;
    [SerializeField] private GameObject cityEntryPrefab;
    [SerializeField] private GameObject cityEntriesPanel;
    [SerializeField] private Button confirmDispatchButton;

    private IList<ITown> citiesList;
    private IList<GameObject> cityEntriesList;
    private GameObject activeCity;

    private void Awake()
    {
        citiesList = new List<ITown>();
        cityEntriesList = new List<GameObject>();
        GetAllCities();

        confirmDispatchButton.onClick.AddListener(DispatchUnit);
    }

    public void GetAllCities()
    {
        citiesList = Town.GetAllTowns();
        IList<ITown> ownedCities = new List<ITown>();

        foreach (ITown city in citiesList)
        {
            if (city.PlayerOwned)
            {
                ownedCities.Add(city);
                cityEntriesList.Add(CreateCityEntry(city));
            }
        }

        citiesList = ownedCities;
    }

    public void SetActiveCity(GameObject cityEntry)
    {
        activeCity = cityEntry;
        confirmDispatchButton.interactable = true;
    }

    public GameObject CreateCityEntry(ITown city)
    {
        CityEntry cityEntryData = CityEntry.CreateInstance(city);

        GameObject cityEntryObject = Instantiate(cityEntryPrefab) as GameObject;
        cityEntryObject.transform.SetParent(cityEntriesPanel.transform);
        cityEntryObject.transform.localRotation = Quaternion.identity;
        cityEntryObject.transform.localScale = Vector3.one;

        cityEntryObject.GetComponent<CityEntryDisplay>().cityEntry = cityEntryData;
        cityEntryObject.GetComponent<CityEntryDisplay>().SetDisplay();
        cityEntryObject.GetComponent<SetActiveCity>().Initialize(SetActiveCity);

        return cityEntryObject;
    }

    public void DispatchUnit()
    {
        CityEntry entryData = activeCity.GetComponent<CityEntryDisplay>().cityEntry;
        ITown dispatchCity = activeCity.GetComponent<CityEntryDisplay>().cityEntry.city;
        IUnit dispatchableUnit = barracks.activeEntry.GetComponent<UnitEntryDisplay>().unitEntry.unit;
        dispatchableUnit.Town = dispatchCity;
        dispatchableUnit.Save();
        barracks.activeEntry.GetComponent<UnitEntryDisplay>().UpdateDisplay(dispatchableUnit);
        unitDisplayController.SetData(dispatchableUnit);
    }
}
