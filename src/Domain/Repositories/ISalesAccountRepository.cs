namespace Linn.SalesAccounts.Domain.Repositories
{
    using System.Collections.Generic;

    public interface ISalesAccountRepository
    {
        SalesAccount GetById(int id);

        SalesAccount GetByAccountId(int accountId);

        IEnumerable<SalesAccount> GetAllOpenAccounts();

        IEnumerable<SalesAccount> Get(string searchTerm);

        void Add(SalesAccount salesAccount);
    }
}
