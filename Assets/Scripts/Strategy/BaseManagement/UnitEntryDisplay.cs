using SwordAndBored.Strategy.BaseManagement.SO;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class UnitEntryDisplay : MonoBehaviour
    {
        public UnitEntry unitEntry;

        public Text EntryRole;

        public Text EntryDescription;

        public Image EntryImage;

        void Start()
        {
            EntryRole.text = unitEntry.RoleName;
            EntryDescription.text = unitEntry.RoleDescription;
            EntryImage.sprite = unitEntry.RoleArtwork;
        }
    }
}