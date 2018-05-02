namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources;

    public class ProposedTurnoverBandsResourceBuilder : IResourceBuilder<IEnumerable<ProposedTurnoverBand>>
    {
        private readonly ProposedTurnoverBandResourceBuilder proposedTurnoverBandResourceBuilder = new ProposedTurnoverBandResourceBuilder();

        public IEnumerable<ProposedTurnoverBandResource> Build(IEnumerable<ProposedTurnoverBand> proposedTurnoverBands)
        {
            return proposedTurnoverBands.Select(s => this.proposedTurnoverBandResourceBuilder.Build(s));
        }

        object IResourceBuilder<IEnumerable<ProposedTurnoverBand>>.Build(IEnumerable<ProposedTurnoverBand> proposedTurnoverBands) => this.Build(proposedTurnoverBands);

        public string GetLocation(IEnumerable<ProposedTurnoverBand> proposedTurnoverBands) => "/sales/accounts/proposed-turnover-bands";
    }
}