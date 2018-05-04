namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.SalesAccounts.Domain.Models;
    using Linn.SalesAccounts.Resources;

    public class TurnoverBandProposalResourceBuilder : IResourceBuilder<TurnoverBandProposal>
    {
        private readonly ProposedTurnoverBandResourceBuilder proposedTurnoverBandResourceBuilder = new ProposedTurnoverBandResourceBuilder();

        public TurnoverBandProposalResource Build(TurnoverBandProposal turnoverBandProposal)
        {
            return new TurnoverBandProposalResource
                       {
                           FinancialYear = turnoverBandProposal.FinancialYear,
                           ProposedTurnoverBands = turnoverBandProposal.ProposedTurnoverBands.Select(s => this.proposedTurnoverBandResourceBuilder.Build(s)),
                           Links = this.BuildLinks(turnoverBandProposal).ToArray()
            };
        }

        object IResourceBuilder<TurnoverBandProposal>.Build(TurnoverBandProposal turnoverBandProposal) => this.Build(turnoverBandProposal);

        public string GetLocation(TurnoverBandProposal turnoverBandProposal) => $"/sales/accounts/turnover-band-proposals?financialYear={turnoverBandProposal.FinancialYear}";

        private IEnumerable<LinkResource> BuildLinks(TurnoverBandProposal turnoverBandProposal)
        {
            yield return new LinkResource("self", this.GetLocation(turnoverBandProposal));

            yield return new LinkResource("apply-proposal", $"{this.GetLocation(turnoverBandProposal)}/apply?financialYear={turnoverBandProposal.FinancialYear}");
        }
    }
}