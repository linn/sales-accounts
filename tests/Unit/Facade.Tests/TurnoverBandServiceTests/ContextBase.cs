namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected TurnoverBandService Sut { get; private set; }

        protected IProposedTurnoverBandService ProposedTurnoverBandService { get; private set; }

        protected IProposedTurnoverBandRepository ProposedTurnoverBandRepository { get; private set; }

        protected IResult<TurnoverBandProposal> Results { get; set; }

        protected IResult<ProposedTurnoverBand> Result { get; set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.ProposedTurnoverBandService = Substitute.For<IProposedTurnoverBandService>();
            this.ProposedTurnoverBandRepository = Substitute.For<IProposedTurnoverBandRepository>();

            this.Sut = new TurnoverBandService(
                this.TransactionManager,
                this.ProposedTurnoverBandService,
                this.ProposedTurnoverBandRepository);
        }
    }
}