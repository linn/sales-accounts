namespace Linn.SalesAccounts.Resources.SalesAccounts
{
    using Linn.Common.Resources;

    public class SalesAccountResource : HypermediaResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TurnoverBandUri { get; set; }

        public string DiscountSchemeUri { get; set; }

        public bool EligibleForGoodCreditDiscount { get; set; }

        public bool EligibleForRebate { get; set; }

        public bool GrowthPartner { get; set; }

        public string ClosedOn { get; set; }
    }
}
