namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SalesAccountService Sut { get; private set; }

        protected ISalesAccountRepository SalesAccountRepository { get; private set; }

        protected IDiscountSchemeService DiscountSchemeService { get; private set; }

        protected IResult<SalesAccount> Result { get; set; }

        protected ITransactionManager TransactionManager { get; private set; }

        protected SalesAccount SalesAccount { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.SalesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "name"));
            this.SalesAccountRepository = Substitute.For<ISalesAccountRepository>();
            this.DiscountSchemeService = Substitute.For<IDiscountSchemeService>();
            this.SalesAccountRepository.GetById(1).Returns(this.SalesAccount);
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.Sut = new SalesAccountService(
                this.TransactionManager,
                this.SalesAccountRepository,
                this.DiscountSchemeService);
        }
    }
}