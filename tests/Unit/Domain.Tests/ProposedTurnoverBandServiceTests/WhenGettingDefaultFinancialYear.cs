namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenGettingDefaultFinancialYear : ContextBase
    {
        private string financialYear;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = this.Sut.DefaultFinancialYear();
        }

        [Test]
        public void ShouldSuggestYear()
        {
            this.financialYear.Should().MatchRegex(@"^\d\d\d\d\/\d\d$");
        }
    }
}
