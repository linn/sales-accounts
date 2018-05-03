namespace Linn.SalesAccounts.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;

    public interface ITurnoverBandService
    {
        IResult<IEnumerable<ProposedTurnoverBand>> ProposeTurnoverBands(string financialYear);

        IResult<ProposedTurnoverBand> GetProposedTurnoverBand(int id);
    }
}