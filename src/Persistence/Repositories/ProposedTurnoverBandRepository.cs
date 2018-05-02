namespace Linn.SalesAccounts.Persistence.Repositories
{
    using System.Collections.Generic;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Persistence;

    public class ProposedTurnoverBandRepository : IProposedTurnoverBandRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProposedTurnoverBandRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProposedTurnoverBand GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProposedTurnoverBand> GetAllForFinancialYear(string financialYear)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ProposedTurnoverBand proposedTurnoverBand)
        {
            throw new System.NotImplementedException();
        }
    }
}
