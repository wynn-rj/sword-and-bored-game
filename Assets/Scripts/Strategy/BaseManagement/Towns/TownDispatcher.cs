using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Buildings;
using SwordAndBored.Strategy.BaseManagement.Units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.StrategyView.BaseManagement.Towns
{
    public class TownDispatcher : MonoBehaviour
    {
        [SerializeField] private Barracks barracks;
        [SerializeField] private DisplayDataController unitDisplayController;
        [SerializeField] private GameObject townEntryPrefab;
        [SerializeField] private GameObject townsEntriesPanel;
        [SerializeField] private Button confirmDispatchButton;

        private IList<ITown> townsList;
        private IList<GameObject> townEntriesList;
        private GameObject activeTown;

        private void Awake()
        {
            townsList = new List<ITown>();
            townEntriesList = new List<GameObject>();
            GetAllTowns();

            confirmDispatchButton.onClick.AddListener(DispatchUnit);
        }

        public void GetAllTowns()
        {
            townsList = Town.GetAllTowns();
            IList<ITown> ownedTowns = new List<ITown>();

            foreach (ITown town in townsList)
            {
                if (town.PlayerOwned)
                {
                    ownedTowns.Add(town);
                    townEntriesList.Add(CreateTownEntry(town));
                }
            }

            townsList = ownedTowns;
        }

        public void SetActiveTown(GameObject townEntry)
        {
            activeTown = townEntry;
            confirmDispatchButton.interactable = true;
        }

        public GameObject CreateTownEntry(ITown town)
        {
            TownEntry townEntryData = TownEntry.CreateInstance(town);

            GameObject townEntryObject = Instantiate(townEntryPrefab) as GameObject;
            townEntryObject.transform.SetParent(townsEntriesPanel.transform);
            townEntryObject.transform.localRotation = Quaternion.identity;
            townEntryObject.transform.localScale = Vector3.one;

            townEntryObject.GetComponent<TownEntryDisplay>().townEntry = townEntryData;
            townEntryObject.GetComponent<TownEntryDisplay>().SetDisplay();
            townEntryObject.GetComponent<SetActiveTown>().Initialize(SetActiveTown);

            return townEntryObject;
        }

        public void DispatchUnit()
        {
            TownEntry entryData = activeTown.GetComponent<TownEntryDisplay>().townEntry;
            ITown dispatchTown = activeTown.GetComponent<TownEntryDisplay>().townEntry.town;
            IUnit dispatchableUnit = barracks.activeEntry.GetComponent<UnitEntryDisplay>().unitEntry.unit;
            dispatchableUnit.Town = dispatchTown;
            dispatchableUnit.Save();
            barracks.activeEntry.GetComponent<UnitEntryDisplay>().UpdateDisplay(dispatchableUnit);
            unitDisplayController.SetData(dispatchableUnit);
        }
    }
}
