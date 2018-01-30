namespace Linn.SalesAccounts.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class SalesAccountService : ISalesAccountService
    {
        private readonly ITransactionManager transactionManager;

        private readonly ISalesAccountRepository salesAccountRepository;

        public SalesAccountService(
            ITransactionManager transactionManager,
            ISalesAccountRepository salesAccountRepository)
        {
            this.transactionManager = transactionManager;
            this.salesAccountRepository = salesAccountRepository;
        }

        public IResult<SalesAccount> GetSalesAccount(int id)
        {
            var account = this.salesAccountRepository.GetById(id);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            return new SuccessResult<SalesAccount>(account);
        }

        public IResult<SalesAccount> AddSalesAccount(SalesAccountCreateResource createResource)
        {
            var createActivity = new SalesAccountCreateActivity(
                createResource.AccountId,
                createResource.OutletNumber,
                createResource.Name,
                string.IsNullOrEmpty(createResource.ClosedOn) ? (DateTime?)null : DateTime.Parse(createResource.ClosedOn));
            var account = new SalesAccount(createActivity);

            this.salesAccountRepository.Add(account);
            this.transactionManager.Commit();

            return new CreatedResult<SalesAccount>(account);
        }

        public IResult<SalesAccount> UpdateSalesAccount(int salesAccountId, SalesAccountUpdateResource updateResource)
        {
            var account = this.salesAccountRepository.GetById(salesAccountId);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            account.UpdateAccount(
                updateResource.DiscountSchemeUri,
                updateResource.TurnoverBandUri,
                updateResource.EligibleForGoodCreditDiscount);
            this.transactionManager.Commit();

            return new SuccessResult<SalesAccount>(account);
        }

        public IResult<SalesAccount> CloseSalesAccount(int salesAccountId, SalesAccountCloseResource closeResource)
        {
            var account = this.salesAccountRepository.GetById(salesAccountId);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            account.CloseAccount(new SalesAccountCloseActivity(DateTime.Parse(closeResource.ClosedOn)));
            this.transactionManager.Commit();

            return new SuccessResult<SalesAccount>(account);
        }
    }
}