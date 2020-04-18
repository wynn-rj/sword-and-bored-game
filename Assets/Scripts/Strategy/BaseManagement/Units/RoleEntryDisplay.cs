using SwordAndBored.Strategy.GameResources;
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
        public Button trainButton;
        [SerializeField] private ResourceManager resourceManager;

        void Start()
        {
            entryRole.text = roleEntry.roleName;
            entryDescription.text = roleEntry.roleDescription;
            entryImage.sprite = roleEntry.roleArtwork;
            entryCost.text = "Cost:" + roleEntry.roleCost.ToString();
        }

        private void Update()
        {
            if (resourceManager.CanAffordPurchase(new Payment("Gold", roleEntry.roleCost)))
            {
                trainButton.interactable = true;
            }
            else
            {
                trainButton.interactable = false;
            }
        }
    }
}