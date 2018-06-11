namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Dispatchers;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SalesAccountService Sut { get; private set; }

        protected ISalesAccountRepository SalesAccountRepository { get; private set; }

        protected IDiscountingService DiscountingService { get; private set; }

        protected IResult<SalesAccount> Result { get; set; }

        protected IResult<IEnumerable<SalesAccount>> Results { get; set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected ISalesAccountUpdatedDispatcher SalesAccountUpdatedDispatcher { get; private set; }

        protected SalesAccount SalesAccount { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SalesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "name"));
            this.SalesAccountRepository = Substitute.For<ISalesAccountRepository>();
            this.SalesAccountUpdatedDispatcher = Substitute.For<ISalesAccountUpdatedDispatcher>();
            this.DiscountingService = Substitute.For<IDiscountingService>();
            this.SalesAccountRepository.GetById(1).Returns(this.SalesAccount);
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.Sut = new SalesAccountService(
                this.TransactionManager,
                this.SalesAccountRepository,
                this.DiscountingService,
                this.SalesAccountUpdatedDispatcher);
        }
    }
}