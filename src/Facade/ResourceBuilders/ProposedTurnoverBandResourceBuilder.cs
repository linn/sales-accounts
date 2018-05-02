namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources;

    public class ProposedTurnoverBandResourceBuilder : IResourceBuilder<ProposedTurnoverBand>
    {
        public ProposedTurnoverBandResource Build(ProposedTurnoverBand proposedTurnoverBand)
        {
            return new ProposedTurnoverBandResource
            {
                SalesAccountId = proposedTurnoverBand.SalesAccount.Id,
                SalesValueBase = proposedTurnoverBand.SalesValueBase,
                SalesValueCurrency = proposedTurnoverBand.SalesValueCurrency,
                FinancialYear = proposedTurnoverBand.FinancialYear,
                CalculatedTurnoverBandUri = proposedTurnoverBand.CalculatedTurnoverBandUri,
                ProposedTurnoverBandUri = proposedTurnoverBand.ProposedTurnoverBandUri,
                IncludeInUpdate = proposedTurnoverBand.IncludeInUpdate,
                Links = this.BuildLinks(proposedTurnoverBand).ToArray()
            };
        }

        object IResourceBuilder<ProposedTurnoverBand>.Build(ProposedTurnoverBand proposedTurnoverBand) => this.Build(proposedTurnoverBand);

        public string GetLocation(ProposedTurnoverBand proposedTurnoverBand) => throw new NotImplementedException();

        private IEnumerable<LinkResource> BuildLinks(ProposedTurnoverBand proposedTurnoverBand)
        {
            yield return new LinkResource("sales-account", $"/sales/accounts/{proposedTurnoverBand.SalesAccount.Id}");
        }
    }
}