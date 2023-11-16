namespace Linn.SalesAccounts.Resources.SalesAccounts
{
    public class SalesAccountUpdateResource
    {
        public string TurnoverBandUri { get; set; }

        public string DiscountSchemeUri { get; set; }

        public bool EligibleForGoodCreditDiscount { get; set; }

        public bool EligibleForRebate { get; set; }

        public bool GrowthPartner { get; set; }

        public bool OnBoardingAccount { get; set; }
    }
}
