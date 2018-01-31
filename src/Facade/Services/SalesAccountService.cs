namespace Linn.SalesAccounts.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Exceptions;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class SalesAccountService : ISalesAccountService
    {
        private readonly ITransactionManager transactionManager;

        private readonly ISalesAccountRepository salesAccountRepository;

        private readonly IDiscountSchemeService discountSchemeService;

        public SalesAccountService(
            ITransactionManager transactionManager,
            ISalesAccountRepository salesAccountRepository,
            IDiscountSchemeService discountSchemeService)
        {
            this.transactionManager = transactionManager;
            this.salesAccountRepository = salesAccountRepository;
            this.discountSchemeService = discountSchemeService;
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

            var discountScheme = this.discountSchemeService.GetDiscountScheme(updateResource.DiscountSchemeUri);
            if (discountScheme == null)
            {
                return new BadRequestResult<SalesAccount>($"Could not find discount scheme {updateResource.DiscountSchemeUri}");
            }

            try
            {
                account.UpdateAccount(
                    discountScheme,
                    updateResource.TurnoverBandUri,
                    updateResource.EligibleForGoodCreditDiscount,
                    updateResource.EligibleForRebate);
                this.transactionManager.Commit();
            }
            catch (InvalidTurnoverBandException exception)
            {
                return new BadRequestResult<SalesAccount>(exception.Message);
            }

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