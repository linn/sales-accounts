namespace Linn.SalesAccounts.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class ProposedTurnoverBandRepository : IProposedTurnoverBandRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProposedTurnoverBandRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProposedTurnoverBand GetById(int id)
        {
            return this.serviceDbContext.ProposedTurnoverBands
                .Include(s => s.SalesAccount)
                .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<ProposedTurnoverBand> GetAllForFinancialYear(string financialYear)
        {
            return this.serviceDbContext.ProposedTurnoverBands
                .Where(s => s.FinancialYear == financialYear)
                .Include(s => s.SalesAccount);
        }

        public void Add(ProposedTurnoverBand proposedTurnoverBand)
        {
            this.serviceDbContext.ProposedTurnoverBands.Add(proposedTurnoverBand);
        }

        public void Remove(ProposedTurnoverBand proposedTurnoverBand)
        {
            this.serviceDbContext.ProposedTurnoverBands.Remove(proposedTurnoverBand);
        }
    }
}
