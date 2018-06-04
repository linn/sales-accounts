namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Domain.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCalculatingTurnoverBands : ContextBase
    {
        private TurnoverBandProposal result;

        private string financialYear;

        private SalesAccount account1, account2, account3, account4;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            this.account1 = new SalesAccount(new SalesAccountCreateActivity(1, "one")) { DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1" };
            this.account2 = new SalesAccount(new SalesAccountCreateActivity(2, "two")) { DiscountSchemeUri = "/ds/2" };
            this.account3 = new SalesAccount(new SalesAccountCreateActivity(3, "three")) { DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1" };
            this.account4 = new SalesAccount(new SalesAccountCreateActivity(4, "four")) { DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1" };

            var discountScheme1 = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandSetUri = "/tbs/1" };
            var discountScheme2 = new DiscountScheme { DiscountSchemeUri = "/ds/2" };
            this.DiscountingService.GetDiscountScheme("/ds/1").Returns(discountScheme1);
            this.DiscountingService.GetDiscountScheme("/ds/2").Returns(discountScheme2);
            this.DiscountingService.GetTurnoverBandForTurnoverValue("/tbs/1", "GBP", 111).Returns("/tb/1");
            this.DiscountingService.GetTurnoverBandForTurnoverValue("/tbs/1", "EUR", 333).Returns("/tb/2");
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear(this.financialYear)
                .Returns(new ProposedTurnoverBand[0]);
            this.SalesReportingService.GetSalesByAccount(this.financialYear)
                .Returns(new List<SalesDataDetail>
                             {
                                new SalesDataDetail { Id = "1", CurrencyValue = 111, CurrencyCode = "GBP", BaseValue = 111 },
                                new SalesDataDetail { Id = "2", CurrencyValue = 222, CurrencyCode = "GBP", BaseValue = 222 },
                                new SalesDataDetail { Id = "3", CurrencyValue = 333, CurrencyCode = "EUR", BaseValue = 333 },
                                new SalesDataDetail { Id = "4", CurrencyValue = 333, CurrencyCode = "NEW", BaseValue = 333 }
                             });
            this.SalesAccountRepository.GetAllOpenAccounts().Returns(new[] { this.account1, this.account2, this.account3, this.account4 });
            this.result = this.Sut.CalculateProposedTurnoverBands(this.financialYear);
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

        [Test]
        public void ShouldReturnYear()
        {
            this.result.FinancialYear.Should().Be(this.financialYear);
        }

        [Test]
        public void ShouldReturnTwoTurnoverBandProposals()
        {
            var results = this.result.ProposedTurnoverBands.ToList();
            results.Should().HaveCount(3);
            var firstProposal = results.First(r => r.SalesAccount.Id == 1);
            firstProposal.ProposedTurnoverBandUri.Should().Be("/tb/1");
            firstProposal.CalculatedTurnoverBandUri.Should().Be("/tb/1");
            firstProposal.SalesValueCurrency.Should().Be(111);
            firstProposal.IncludeInUpdate.Should().BeTrue();
            firstProposal.FinancialYear.Should().Be(this.financialYear);
            firstProposal.SalesValueBase.Should().Be(111);
            firstProposal.AppliedToAccount.Should().BeFalse();
            firstProposal.CurrencyCode.Should().Be("GBP");
            var secondProposal = results.First(r => r.SalesAccount.Id == 3);
            secondProposal.ProposedTurnoverBandUri.Should().Be("/tb/2");
            secondProposal.CalculatedTurnoverBandUri.Should().Be("/tb/2");
            secondProposal.SalesValueCurrency.Should().Be(333);
            secondProposal.IncludeInUpdate.Should().BeTrue();
            secondProposal.FinancialYear.Should().Be(this.financialYear);
            secondProposal.SalesValueBase.Should().Be(333);
            secondProposal.AppliedToAccount.Should().BeFalse();
            secondProposal.CurrencyCode.Should().Be("EUR");
            var thirdProposal = results.First(r => r.SalesAccount.Id == 4);
            thirdProposal.ProposedTurnoverBandUri.Should().Be(string.Empty);
            thirdProposal.CalculatedTurnoverBandUri.Should().Be(string.Empty);
            thirdProposal.SalesValueCurrency.Should().Be(333);
            thirdProposal.IncludeInUpdate.Should().BeTrue();
            thirdProposal.FinancialYear.Should().Be(this.financialYear);
            thirdProposal.SalesValueBase.Should().Be(333);
            thirdProposal.AppliedToAccount.Should().BeFalse();
            thirdProposal.CurrencyCode.Should().Be("NEW");
        }
    }
}
