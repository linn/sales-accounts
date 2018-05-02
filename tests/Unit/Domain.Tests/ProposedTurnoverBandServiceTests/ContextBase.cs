namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ISalesReportingService SalesReportingService { get; private set; }

        protected IDiscountingService DiscountingService { get; private set; }

        protected IProposedTurnoverBandRepository ProposedTurnoverBandRepository { get; private set; }

        protected ISalesAccountRepository SalesAccountRepository { get; private set; }

        protected ProposedTurnoverBandService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProposedTurnoverBandRepository = Substitute.For<IProposedTurnoverBandRepository>();
            this.SalesAccountRepository = Substitute.For<ISalesAccountRepository>();
            this.SalesReportingService = Substitute.For<ISalesReportingService>();
            this.DiscountingService = Substitute.For<IDiscountingService>();

            this.Sut = new ProposedTurnoverBandService(
                this.ProposedTurnoverBandRepository,
                this.SalesAccountRepository,
                this.SalesReportingService,
                this.DiscountingService);
        }
    }
}