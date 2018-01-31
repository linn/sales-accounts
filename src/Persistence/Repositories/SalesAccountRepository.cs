﻿namespace Linn.SalesAccounts.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Persistence;

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
                .SingleOrDefault(t => t.Id == id);
        }

        public SalesAccount GetByAccountId(int accountId)
        {
            return this.serviceDbContext.SalesAccounts
                .SingleOrDefault(t => t.AccountId == accountId);
        }

        public IEnumerable<SalesAccount> GetAllOpenAccounts()
        {
            return this.serviceDbContext.SalesAccounts
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