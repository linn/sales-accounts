namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
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
                SalesValueBase = proposedTurnoverBand.SalesValueBase,
                SalesValueCurrency = proposedTurnoverBand.SalesValueCurrency,
                FinancialYear = proposedTurnoverBand.FinancialYear,
                CalculatedTurnoverBandUri = proposedTurnoverBand.CalculatedTurnoverBandUri,
                ProposedTurnoverBandUri = proposedTurnoverBand.ProposedTurnoverBandUri,
                IncludeInUpdate = proposedTurnoverBand.IncludeInUpdate,
                AppliedToAccount = proposedTurnoverBand.AppliedToAccount,
                Links = this.BuildLinks(proposedTurnoverBand).ToArray()
            };
        }

        object IResourceBuilder<ProposedTurnoverBand>.Build(ProposedTurnoverBand proposedTurnoverBand) => this.Build(proposedTurnoverBand);

        public string GetLocation(ProposedTurnoverBand proposedTurnoverBand) => $"/sales/accounts/proposed-turnover-bands/{proposedTurnoverBand.Id}";

        private IEnumerable<LinkResource> BuildLinks(ProposedTurnoverBand proposedTurnoverBand)
        {
            yield return new LinkResource("sales-account", $"/sales/accounts/{proposedTurnoverBand.SalesAccount.Id}");

            yield return new LinkResource("self", $"/sales/accounts/turnover-band-proposals/details/{proposedTurnoverBand.Id}");
        }
    }
}