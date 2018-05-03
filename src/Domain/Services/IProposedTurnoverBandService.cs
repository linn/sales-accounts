namespace Linn.SalesAccounts.Domain.Services
{
    using System.Collections.Generic;

    public interface IProposedTurnoverBandService
    {
        IEnumerable<ProposedTurnoverBand> CalculateProposedTurnoverBands(string financialYear);

        string DefaultFinancialYear();
    }
}
