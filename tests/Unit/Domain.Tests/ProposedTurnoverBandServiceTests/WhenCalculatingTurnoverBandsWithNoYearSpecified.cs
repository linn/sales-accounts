namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using NSubstitute;

    using NUnit.Framework;

    public class WhenCalculatingTurnoverBandsWithNoYearSpecified : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.CalculateProposedTurnoverBands(null);
        }

        [Test]
        public void ShouldUseCalculatedFinancialYear()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.Sut.DefaultFinancialYear());
        }
    }
}
