using UnityEngine;
using SwordAndBored.Strategy.Transitions;
using SwordAndBored.GameData;

namespace SwordAndBored.Strategy.GameResources
{
    public class ResourceManager : MonoBehaviour
    {
        public int GoldAmount
        {
            get { return gold; }
            set
            {
                gold = value;
                resourceDisplay.UpdateDisplay(gold);
            }
        }
        public ResourceDisplay resourceDisplay;
        private int gold;

        private void Start()
        {
            GoldAmount = ResourceHelper.GetGoldAmount();
        }

        public bool CanAffordPurchase(IPayment payment)
        {
            return payment.Cost <= GoldAmount;
        }

        public void MakePayment(IPayment payment)
        {
            if (CanAffordPurchase(payment))
            {
                GoldAmount -= payment.Cost;
            }
        }

        private void OnDestroy()
        {
            ResourceHelper.SetGoldAmount(GoldAmount);
        }
    }
}
