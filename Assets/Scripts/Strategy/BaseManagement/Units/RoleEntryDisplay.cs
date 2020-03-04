using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class RoleEntryDisplay : MonoBehaviour
    {
        public RoleEntry roleEntry;
        public Text entryRole;
        public Text entryDescription;
        public Image entryImage;

        void Start()
        {
            entryRole.text = roleEntry.RoleName;
            entryDescription.text = roleEntry.RoleDescription;
            entryImage.sprite = roleEntry.RoleArtwork;
        }
    }
}