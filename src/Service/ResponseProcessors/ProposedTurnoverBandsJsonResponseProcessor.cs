namespace Linn.SalesAccounts.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.SalesAccounts.Domain;

    public class ProposedTurnoverBandsJsonResponseProcessor : JsonResponseProcessor<IEnumerable<ProposedTurnoverBand>>
    {
        public ProposedTurnoverBandsJsonResponseProcessor(IResourceBuilder<IEnumerable<ProposedTurnoverBand>> resourceBuilder)
            : base(resourceBuilder, "sales.account-proposed-turnover-bands", 1)
        {
        }
    }
}