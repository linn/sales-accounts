namespace Linn.SalesAccounts.Domain.Dispatchers.Messages
{
    public class SalesAccountMessage
    {
        public int Id { get; set; }

        public string Href { get; set; }

        public string Name { get; set; }

        public string TurnoverBandUri { get; set; }

        public string DiscountSchemeUri { get; set; }

        public bool EligibleForGoodCreditDiscount { get; set; }

        public bool EligibleForRebate { get; set; }

        public bool GrowthPartner { get; set; }

        public string ClosedOn { get; set; }

        public bool OnBoardingAccount { get; set; }
    }
}
