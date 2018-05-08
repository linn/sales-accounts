namespace Linn.SalesAccounts.Domain.Services
{
    using Linn.SalesAccounts.Domain.Models;

    public interface IProposedTurnoverBandService
    {
        TurnoverBandProposal CalculateProposedTurnoverBands(string financialYear);

        TurnoverBandProposal ApplyTurnoverBandProposal(string financialYear);

        string DefaultFinancialYear();
    }
}
