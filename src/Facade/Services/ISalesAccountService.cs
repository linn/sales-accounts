namespace Linn.SalesAccounts.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public interface ISalesAccountService
    {
        IResult<SalesAccount> GetById(int id);

        IResult<IEnumerable<SalesAccount>> Get(string searchTerm);

        IResult<SalesAccount> AddSalesAccount(SalesAccountCreateResource createResource);

        IResult<SalesAccount> UpdateSalesAccount(int salesAccountId, SalesAccountUpdateResource updateResource);

        IResult<SalesAccount> UpdateSalesAccountName(int salesAccountId, string name);

        IResult<SalesAccount> CloseSalesAccount(int salesAccountId, SalesAccountCloseResource closeResource);
    }
}