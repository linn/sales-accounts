namespace Linn.SalesAccounts.Resources
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class TurnoverBandProposalResource : HypermediaResource
    {
        public string FinancialYear { get; set; }

        public IEnumerable<ProposedTurnoverBandResource> ProposedTurnoverBands { get; set; }
    }
}
