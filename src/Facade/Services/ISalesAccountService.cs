namespace Linn.SalesAccounts.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public interface ISalesAccountService
    {
        IResult<SalesAccount> GetSalesAccount(int id);

        IResult<SalesAccount> AddSalesAccount(SalesAccountCreateResource createResource);

        IResult<SalesAccount> UpdateSalesAccount(int salesAccountId, SalesAccountUpdateResource updateResource);

        IResult<SalesAccount> UpdateSalesAccountName(int salesAccountId, string name);

        IResult<SalesAccount> CloseSalesAccount(int salesAccountId, SalesAccountCloseResource closeResource);
    }
}