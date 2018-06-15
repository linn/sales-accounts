namespace Linn.SalesAccounts.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public interface ISalesAccountService
    {
        IResult<SalesAccount> GetById(int id);

        IResult<IEnumerable<SalesAccount>> Get(string searchTerm);

        IResult<SalesAccount> AddSalesAccount(SalesAccountCreateResource createResource, string employeeUri);

        IResult<SalesAccount> UpdateSalesAccount(int salesAccountId, SalesAccountUpdateResource updateResource, string employeeUri);

        IResult<SalesAccount> UpdateSalesAccountNameAndAddress(int salesAccountId, string name, AddressResource address, string employeeUri);

        IResult<SalesAccount> CloseSalesAccount(int salesAccountId, SalesAccountCloseResource closeResource, string employeeUri);

        IResult<IEnumerable<SalesAccountActivity>> GetActivitiesById(int id);
    }
}