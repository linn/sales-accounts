namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCalculatingTurnoverBandsWithNoYearSpecified : ContextBase
    {
        private string calculatedYear;

        [SetUp]
        public void SetUp()
        {
            var date = new DateTime(DateTime.Now.Year, 1, 1);
            this.calculatedYear = $"{date.AddYears(-1).Year}/{date:yy}";
            this.Sut.CalculateProposedTurnoverBands(null);
        }

        [Test]
        public void ShouldUseCalculatedFinancialYear()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.calculatedYear);
        }
    }
}
