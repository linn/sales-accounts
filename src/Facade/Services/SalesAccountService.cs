using Linn.SalesAccounts.Facade.Extensions;

namespace Linn.SalesAccounts.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Dispatchers;
    using Linn.SalesAccounts.Domain.Dispatchers.Extensions;
    using Linn.SalesAccounts.Domain.Exceptions;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Resources.Messaging;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class SalesAccountService : ISalesAccountService
    {
        private readonly ITransactionManager transactionManager;

        private readonly ISalesAccountRepository salesAccountRepository;

        private readonly IDiscountSchemeService discountSchemeService;

        private readonly ISalesAccountUpdatedDispatcher salesAccountUpdatedDispatcher;

        public SalesAccountService(
            ITransactionManager transactionManager,
            ISalesAccountRepository salesAccountRepository,
            IDiscountSchemeService discountSchemeService,
            ISalesAccountUpdatedDispatcher salesAccountUpdatedDispatcher)
        {
            this.transactionManager = transactionManager;
            this.salesAccountRepository = salesAccountRepository;
            this.discountSchemeService = discountSchemeService;
            this.salesAccountUpdatedDispatcher = salesAccountUpdatedDispatcher;
        }

        public IResult<SalesAccount> GetById(int id)
        {
            var account = this.salesAccountRepository.GetById(id);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            return new SuccessResult<SalesAccount>(account);
        }

        public IResult<IEnumerable<SalesAccount>> Get(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new SuccessResult<IEnumerable<SalesAccount>>(this.salesAccountRepository.GetAllOpenAccounts());
            }

            var accounts = this.salesAccountRepository.Get(searchTerm).ToList();
            if (!int.TryParse(searchTerm, out var accountIdSearch))
            {
                return new SuccessResult<IEnumerable<SalesAccount>>(accounts);
            }

            var account = this.salesAccountRepository.GetById(accountIdSearch);
            if (account != null)
            {
                accounts.Add(account);
            }

            return new SuccessResult<IEnumerable<SalesAccount>>(accounts);
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
            if (!string.IsNullOrEmpty(updateResource.DiscountSchemeUri) && discountScheme == null)
            {
                return new BadRequestResult<SalesAccount>($"Could not find discount scheme {updateResource.DiscountSchemeUri}");
            }

            try
            {
                account.UpdateAccount(
                    discountScheme,
                    updateResource.TurnoverBandUri,
                    updateResource.EligibleForGoodCreditDiscount,
                    updateResource.EligibleForRebate,
                    updateResource.GrowthPartner);

                this.transactionManager.Commit();
                this.salesAccountUpdatedDispatcher.SendSalesAccountUpdated(account.ToMessage());
            }
            catch (InvalidTurnoverBandException exception)
            {
                return new BadRequestResult<SalesAccount>(exception.Message);
            }

            return new SuccessResult<SalesAccount>(account);
        }

        public IResult<SalesAccount> UpdateSalesAccountNameAndAddress(int salesAccountId, string name, AddressResource address)
        {
            var account = this.salesAccountRepository.GetById(salesAccountId);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            account.UpdateNameAndAddress(name, address.ToDomain());

            this.transactionManager.Commit();
            this.salesAccountUpdatedDispatcher.SendSalesAccountUpdated(account.ToMessage());

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