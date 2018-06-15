namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRecalculatingAppliedTurnoverBands : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> results;

        private string financialYear;

        private SalesAccount account1;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            this.account1 = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "one"))
                                {
                                    DiscountSchemeUri = "/ds/1",
                                    TurnoverBandUri = "/tb/1"
                                };

            var proposedTurnoverBands = new List<ProposedTurnoverBand>
                                            {
                                                new ProposedTurnoverBand
                                                    {
                                                        SalesAccount = this.account1,
                                                        FinancialYear = this.financialYear,
                                                        AppliedToAccount = true,
                                                        SalesValueCurrency = 1,
                                                        ProposedTurnoverBandUri = "/tb/100",
                                                        CalculatedTurnoverBandUri = "/tb/100"
                                                    }
                                            };
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear(this.financialYear)
                .Returns(proposedTurnoverBands);
            var discountScheme1 = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandSetUri = "/tbs/1" };
            this.DiscountingService.GetDiscountScheme("/ds/1").Returns(discountScheme1);
            this.DiscountingService.GetTurnoverBandForTurnoverValue("/tbs/1", "GBP", 111).Returns("/tb/1");
            this.SalesReportingService.GetSalesByAccount(this.financialYear)
                .Returns(new List<SalesDataDetail>
                             {
                                new SalesDataDetail { Id = "1", CurrencyValue = 111, CurrencyCode = "GBP", BaseValue = 111 }
                             });
            this.SalesAccountRepository.GetAllOpenAccounts().Returns(new[] { this.account1 });
            this.results = this.Sut.CalculateProposedTurnoverBands(this.financialYear).ProposedTurnoverBands;
        }

        [Test]
        public void ShouldGetExistingCalculations()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.financialYear);
        }

        [Test]
        public void ShouldNotChangeTurnoverBand()
        {
            this.results.Should().HaveCount(1);
            var firstProposal = this.results.First(r => r.SalesAccount.Id == 1);
            firstProposal.ProposedTurnoverBandUri.Should().Be("/tb/100");
            firstProposal.CalculatedTurnoverBandUri.Should().Be("/tb/100");
            firstProposal.SalesValueCurrency.Should().Be(1);
            firstProposal.AppliedToAccount.Should().BeTrue();
            firstProposal.FinancialYear.Should().Be(this.financialYear);
        }
    }
}
