namespace Linn.SalesAccounts.Service.Tests.TurnoverBandModuleTests
{
    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;
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
        protected ITurnoverBandService TurnoverBandService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.TurnoverBandService = Substitute.For<ITurnoverBandService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.TurnoverBandService);
                        with.Dependency<IResourceBuilder<ProposedTurnoverBand>>(new ProposedTurnoverBandResourceBuilder());
                        with.Dependency<IResourceBuilder<TurnoverBandProposal>>(new TurnoverBandProposalResourceBuilder());
                        with.Module<TurnoverBandModule>();
                        with.ResponseProcessor<ProposedTurnoverBandJsonResponseProcessor>();
                        with.ResponseProcessor<TurnoverBandProposalJsonResponseProcessor>();
                        with.ResponseProcessor<ProposedTurnoverBandsCsvResponseProcessor>();
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}