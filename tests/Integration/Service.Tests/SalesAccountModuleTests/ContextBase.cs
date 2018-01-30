namespace Linn.SalesAccounts.Service.Tests.SalesAccountModuleTests
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Repositories;
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
        protected ISalesAccountRepository SalesAccountRepository { get; set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.SalesAccountRepository = Substitute.For<ISalesAccountRepository>();
            this.TransactionManager = Substitute.For<ITransactionManager>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.SalesAccountRepository);
                        with.Dependency(this.TransactionManager);
                        with.Dependency<SalesAccountService>();
                        with.Dependency<IResourceBuilder<SalesAccount>>(new SalesAccountResourceBuilder());
                        with.Module<SalesAccountModule>();
                        with.ResponseProcessor<SalesAccountJsonResponseProcessor>();
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}