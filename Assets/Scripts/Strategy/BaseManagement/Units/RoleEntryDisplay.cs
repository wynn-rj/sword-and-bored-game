using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class RoleEntryDisplay : MonoBehaviour
    {
        public RoleEntry roleEntry;
        public Text entryRole;
        public Text entryDescription;
        public Text entryCost;
        public Image entryImage;

        void Start()
        {
            entryRole.text = roleEntry.roleName;
            entryDescription.text = roleEntry.roleDescription;
            entryImage.sprite = roleEntry.roleArtwork;
            entryCost.text = "Cost:" + roleEntry.roleCost.ToString();
        }
    }
}