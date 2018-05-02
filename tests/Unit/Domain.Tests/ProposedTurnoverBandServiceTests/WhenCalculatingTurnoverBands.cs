namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System.Collections.Generic;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCalculatingTurnoverBands : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> results;

        private string financialYear;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            this.results = this.Sut.CalculateProposedTurnoverBands(this.financialYear);
        }

        [Test]
        public void ShouldGetExistingCalculations()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.financialYear);
        }

        [Test]
        public void ShouldGetSalesData()
        {
            this.SalesReportingService.Received().GetSalesByAccount(this.financialYear);
        }

        [Test]
        public void ShouldGetOpenSalesAccounts()
        {
            this.SalesAccountRepository.Received().GetAllOpenAccounts();
        }
    }
}
