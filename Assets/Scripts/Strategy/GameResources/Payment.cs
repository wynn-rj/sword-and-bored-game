using System;

namespace SwordAndBored.Strategy.GameResources
{
    public class Payment : IPayment
    {
        public String Resource { get; set; }
        public int Cost { get; set; }

        public Payment(string resourceType, int amount)
        {
            this.Resource = resourceType;
            this.Cost = amount;
        }
    }
}
