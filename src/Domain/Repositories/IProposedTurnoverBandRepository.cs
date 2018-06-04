namespace Linn.SalesAccounts.Domain.Repositories
{
    using System.Collections.Generic;

    using Linn.SalesAccounts.Domain;

    public interface IProposedTurnoverBandRepository
    {
        ProposedTurnoverBand GetById(int id);

        IEnumerable<ProposedTurnoverBand> GetAllForFinancialYear(string financialYear);

        void Add(ProposedTurnoverBand proposedTurnoverBand);

        void Remove(ProposedTurnoverBand proposedTurnoverBand);
    }
}
