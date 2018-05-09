namespace Linn.SalesAccounts.Resources
{
    using Linn.Common.Resources;

    public class ProposedTurnoverBandResource : HypermediaResource
    {
        public string FinancialYear { get; set; }

        public decimal SalesValueBase { get; set; }

        public decimal SalesValueCurrency { get; set; }

        public string CalculatedTurnoverBandUri { get; set; }

        public string ProposedTurnoverBandUri { get; set; }

        public bool IncludeInUpdate { get; set; }

        public bool AppliedToAccount { get; set; }
    }
}
