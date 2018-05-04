namespace Linn.SalesAccounts.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.SalesAccounts.Domain.Models;

    public class TurnoverBandProposalJsonResponseProcessor : JsonResponseProcessor<TurnoverBandProposal>
    {
        public TurnoverBandProposalJsonResponseProcessor(IResourceBuilder<TurnoverBandProposal> resourceBuilder)
            : base(resourceBuilder, "sales.accounts-turnover-band-proposal", 1)
        {
        }
    }
}