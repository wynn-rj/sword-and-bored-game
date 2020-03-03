using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class RoleEntryDisplay : MonoBehaviour
    {
        public RoleEntry RoleEntry;

        public Text EntryRole;

        public Text EntryDescription;

        public Image EntryImage;

        void Start()
        {
            EntryRole.text = RoleEntry.RoleName;
            EntryDescription.text = RoleEntry.RoleDescription;
            EntryImage.sprite = RoleEntry.RoleArtwork;
        }
    }
}