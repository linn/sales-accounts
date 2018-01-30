namespace Linn.SalesAccounts.Domain.Repositories
{
    public interface ISalesAccountRepository
    {
        SalesAccount GetById(int id);

        void Add(SalesAccount salesAccount);
    }
}
