namespace Linn.SalesAccounts.Service.Tests.SalesAccountModuleTests
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Dispatchers;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Facade.ResourceBuilders;
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Service.Modules;
    using Linn.SalesAccounts.Service.ResponseProcessors;
    using Linn.SalesAccounts.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected ISalesAccountRepository SalesAccountRepository { get; private set; }

        protected ISalesAccountUpdatedDispatcher SalesAccountUpdatedDispatcher { get; private set; }

        protected IDiscountSchemeService DiscountSchemeService { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.SalesAccountRepository = Substitute.For<ISalesAccountRepository>();
            this.DiscountSchemeService = Substitute.For<IDiscountSchemeService>();
            this.SalesAccountUpdatedDispatcher = Substitute.For<ISalesAccountUpdatedDispatcher>();
            this.TransactionManager = Substitute.For<ITransactionManager>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.SalesAccountRepository);
                        with.Dependency(this.SalesAccountUpdatedDispatcher);
                        with.Dependency(this.DiscountSchemeService);
                        with.Dependency(this.TransactionManager);
                        with.Dependency<SalesAccountService>();
                        with.Dependency<IResourceBuilder<SalesAccount>>(new SalesAccountResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<SalesAccount>>>(new SalesAccountsResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<SalesAccountActivity>>>(new SalesAccountActivitiesResourceBuilder());
                        with.Module<SalesAccountModule>();
                        with.ResponseProcessor<SalesAccountJsonResponseProcessor>();
                        with.ResponseProcessor<SalesAccountsJsonResponseProcessor>();
                        with.ResponseProcessor<SalesAcountActivitiesJsonResponseProcessor>();
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}