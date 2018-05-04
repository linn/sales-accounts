namespace Linn.SalesAccounts.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;

    public interface ITurnoverBandService
    {
        IResult<TurnoverBandProposal> ProposeTurnoverBands(string financialYear);

        IResult<ProposedTurnoverBand> GetProposedTurnoverBand(int id);

        IResult<TurnoverBandProposal> GetProposedTurnoverBands(string financialYear);

        IResult<ProposedTurnoverBand> OverrideTurnoverBand(int id, string turnoverBandUri);
    }
}