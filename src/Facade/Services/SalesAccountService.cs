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
    using Linn.SalesAccounts.Facade.Extensions;
    using Linn.SalesAccounts.Resources;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class SalesAccountService : ISalesAccountService
    {
        private readonly ITransactionManager transactionManager;

        private readonly ISalesAccountRepository salesAccountRepository;

        private readonly IDiscountingService discountingService;

        private readonly ISalesAccountUpdatedDispatcher salesAccountUpdatedDispatcher;

        public SalesAccountService(
            ITransactionManager transactionManager,
            ISalesAccountRepository salesAccountRepository,
            IDiscountingService discountingService,
            ISalesAccountUpdatedDispatcher salesAccountUpdatedDispatcher)
        {
            this.transactionManager = transactionManager;
            this.salesAccountRepository = salesAccountRepository;
            this.discountingService = discountingService;
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

        public IResult<SalesAccount> AddSalesAccount(SalesAccountCreateResource createResource, string updatedByUri)
        {
            var createActivity = new SalesAccountCreateActivity(
                updatedByUri,
                createResource.AccountId,
                createResource.Name,
                string.IsNullOrEmpty(createResource.ClosedOn) ? (DateTime?)null : DateTime.Parse(createResource.ClosedOn));
            var account = new SalesAccount(createActivity);

            this.salesAccountRepository.Add(account);
            this.transactionManager.Commit();

            return new CreatedResult<SalesAccount>(account);
        }

        public IResult<SalesAccount> UpdateSalesAccount(int salesAccountId, SalesAccountUpdateResource updateResource, string updatedByUri)
        {
            var account = this.salesAccountRepository.GetById(salesAccountId);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            var discountScheme = this.discountingService.GetDiscountScheme(updateResource.DiscountSchemeUri);
            if (!string.IsNullOrEmpty(updateResource.DiscountSchemeUri) && discountScheme == null)
            {
                return new BadRequestResult<SalesAccount>($"Could not find discount scheme {updateResource.DiscountSchemeUri}");
            }

            try
            {
                account.UpdateAccount(
                    updatedByUri,
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

        public IResult<SalesAccount> UpdateSalesAccountNameAndAddress(int salesAccountId, string name, AddressResource address, string updatedByUri)
        {
            var account = this.salesAccountRepository.GetById(salesAccountId);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            if (address == null)
            {
                return new BadRequestResult<SalesAccount>("Address cannot be empty.");
            }

            account.UpdateNameAndAddress(updatedByUri, name, address.ToDomain());

            this.transactionManager.Commit();
            this.salesAccountUpdatedDispatcher.SendSalesAccountUpdated(account.ToMessage());

            return new SuccessResult<SalesAccount>(account);
        }

        public IResult<SalesAccount> CloseSalesAccount(int salesAccountId, SalesAccountCloseResource closeResource, string updatedByUri)
        {
            var account = this.salesAccountRepository.GetById(salesAccountId);
            if (account == null)
            {
                return new NotFoundResult<SalesAccount>();
            }

            account.CloseAccount(new SalesAccountCloseActivity(updatedByUri, DateTime.Parse(closeResource.ClosedOn)));
            this.transactionManager.Commit();

            return new SuccessResult<SalesAccount>(account);
        }

        public IResult<IEnumerable<SalesAccountActivity>> GetActivitiesById(int id)
        {
            var account = this.salesAccountRepository.GetById(id);
            if (account == null)
            {
                return new NotFoundResult<IEnumerable<SalesAccountActivity>>();
            }

            var activities = account.Activities;

            return new SuccessResult<IEnumerable<SalesAccountActivity>>(activities);
        }
    }
}