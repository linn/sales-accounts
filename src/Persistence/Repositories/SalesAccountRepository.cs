namespace Linn.SalesAccounts.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class SalesAccountRepository : ISalesAccountRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesAccountRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesAccount GetById(int id)
        {
            return this.serviceDbContext.SalesAccounts
                .Include(s => s.Address)
                .Include(s => s.Activities)
                .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<SalesAccount> GetAllOpenAccounts()
        {
            return this.serviceDbContext.SalesAccounts
                .Include(s => s.Address)
                .Where(s => s.ClosedOn == null);
        }

        public IEnumerable<SalesAccount> Get(string searchTerm)
        {
            return this.serviceDbContext.SalesAccounts
                .Where(s => s.Name.ToLower().Contains(searchTerm.ToLower()));
        }

        public void Add(SalesAccount salesAccount)
        {
            this.serviceDbContext.SalesAccounts.Add(salesAccount);
        }
    }
}
