namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected TurnoverBandService Sut { get; private set; }

        protected IProposedTurnoverBandService ProposedTurnoverBandService { get; private set; }

        protected IResult<IEnumerable<ProposedTurnoverBand>> Results { get; set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.ProposedTurnoverBandService = Substitute.For<IProposedTurnoverBandService>();

            this.Sut = new TurnoverBandService(this.TransactionManager, this.ProposedTurnoverBandService);
        }
    }
}