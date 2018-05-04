namespace Linn.SalesAccounts.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.SalesAccounts.Domain;

    public class ProposedTurnoverBandJsonResponseProcessor : JsonResponseProcessor<ProposedTurnoverBand>
    {
        public ProposedTurnoverBandJsonResponseProcessor(IResourceBuilder<ProposedTurnoverBand> resourceBuilder)
            : base(resourceBuilder, "sales.account-proposed-turnover-band", 1)
        {
        }
    }
}