using TMPro;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Towns
{
    public class TownEntryDisplay : MonoBehaviour
    {
        public TownEntry townEntry;
        public TMP_Text townName;

        public void SetDisplay()
        {
            townName.text = townEntry.townName;
        }
    }
}
