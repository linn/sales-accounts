namespace Linn.SalesAccounts.Domain.Models
{
    using System.Collections.Generic;

    public class TurnoverBandProposal
    {
        public TurnoverBandProposal(string financialYear, IEnumerable<ProposedTurnoverBand> proposedTurnoverBands)
        {
            this.FinancialYear = financialYear;
            this.ProposedTurnoverBands = proposedTurnoverBands;
        }

        public string FinancialYear { get; }

        public IEnumerable<ProposedTurnoverBand> ProposedTurnoverBands { get; }
    }
}
