namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRecalculatingTurnoverBandsWithInvalidAccount : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> results;

        private string financialYear;

        private SalesAccount account1, account2, account3, account4;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            this.account1 = new SalesAccount(new SalesAccountCreateActivity(1, "one")) { DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1" };
            this.account2 = new SalesAccount(new SalesAccountCreateActivity(2, "two"));
            this.account3 = new SalesAccount(new SalesAccountCreateActivity(3, "three")) { DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1" };
            this.account4 = new SalesAccount(new SalesAccountCreateActivity(4, "four")) { DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1" };

            var proposedTurnoverBands = new List<ProposedTurnoverBand>
                                            {
                                                new ProposedTurnoverBand
                                                    {
                                                        SalesAccount = this.account1, FinancialYear = this.financialYear, SalesValueCurrency = 1
                                                    },
                                                new ProposedTurnoverBand
                                                    {
                                                        SalesAccount = this.account2,
                                                        FinancialYear = this.financialYear,
                                                        SalesValueCurrency = 1m,
                                                        SalesValueBase = 1m,
                                                        CalculatedTurnoverBandUri = "/tb/9",
                                                        ProposedTurnoverBandUri = "/tb/9"
                                                    },
                                                new ProposedTurnoverBand
                                                    {
                                                        SalesAccount = this.account3, FinancialYear = this.financialYear, SalesValueCurrency = 1
                                                    }
                                            }.AsEnumerable();
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear(Arg.Any<string>())
                .Returns(proposedTurnoverBands);
            var discountScheme1 = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandSetUri = "/tbs/1" };
            var discountScheme2 = new DiscountScheme { DiscountSchemeUri = "/ds/2" };
            this.DiscountingService.GetDiscountScheme("/ds/1").Returns(discountScheme1);
            this.DiscountingService.GetDiscountScheme("/ds/2").Returns(discountScheme2);
            this.DiscountingService.GetTurnoverBandForTurnoverValue("/tbs/1", "GBP", 111).Returns("/tb/1");
            this.DiscountingService.GetTurnoverBandForTurnoverValue("/tbs/1", "GBP", 222).Returns("/tb/1");
            this.DiscountingService.GetTurnoverBandForTurnoverValue("/tbs/1", "GBP", 333).Returns("/tb/2");
            this.DiscountingService.GetTurnoverBandForTurnoverValue("/tbs/1", "GBP", 444).Returns("/tb/2");
            this.SalesReportingService.GetSalesByAccount(this.financialYear)
                .Returns(new List<SalesDataDetail>
                             {
                                new SalesDataDetail { Id = "1", CurrencyValue = 111, CurrencyCode = "GBP", BaseValue = 111 },
                                new SalesDataDetail { Id = "2", CurrencyValue = 222, CurrencyCode = "GBP", BaseValue = 222 },
                                new SalesDataDetail { Id = "4", CurrencyValue = 444, CurrencyCode = "GBP", BaseValue = 444 }
                             });
            this.SalesAccountRepository.GetAllOpenAccounts().Returns(new[] { this.account1, this.account2, this.account3, this.account4 });
            this.results = this.Sut.CalculateProposedTurnoverBands(this.financialYear).ProposedTurnoverBands;
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
        public void ShouldRemoveProposalsForInvalidAccounts()
        {
            this.ProposedTurnoverBandRepository.Received(1).Remove(Arg.Any<ProposedTurnoverBand>());
        }

        [Test]
        public void ShouldReturnTurnoverBandProposals()
        {
            this.results.Should().HaveCount(3);
            var firstProposal = this.results.First(r => r.SalesAccount.Id == 1);
            firstProposal.ProposedTurnoverBandUri.Should().Be("/tb/1");
            firstProposal.CalculatedTurnoverBandUri.Should().Be("/tb/1");
            firstProposal.SalesValueCurrency.Should().Be(111);
            firstProposal.IncludeInUpdate.Should().BeTrue();
            firstProposal.FinancialYear.Should().Be(this.financialYear);
            firstProposal.SalesValueBase.Should().Be(111);
            var thirdProposal = this.results.First(r => r.SalesAccount.Id == 3);
            thirdProposal.ProposedTurnoverBandUri.Should().Be(string.Empty);
            thirdProposal.CalculatedTurnoverBandUri.Should().Be(string.Empty);
            thirdProposal.SalesValueCurrency.Should().Be(0);
            thirdProposal.IncludeInUpdate.Should().BeTrue();
            thirdProposal.FinancialYear.Should().Be(this.financialYear);
            thirdProposal.SalesValueBase.Should().Be(0);
            var fourthProposal = this.results.First(r => r.SalesAccount.Id == 4);
            fourthProposal.ProposedTurnoverBandUri.Should().Be("/tb/2");
            fourthProposal.CalculatedTurnoverBandUri.Should().Be("/tb/2");
            fourthProposal.SalesValueCurrency.Should().Be(444);
            fourthProposal.IncludeInUpdate.Should().BeTrue();
            fourthProposal.FinancialYear.Should().Be(this.financialYear);
            fourthProposal.SalesValueBase.Should().Be(444);
        }
    }
}
