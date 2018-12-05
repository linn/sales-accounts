namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenApplyingTurnoverBands : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> results;

        private string financialYear;

        private SalesAccount account1, account2, account3, account4;

        private List<ProposedTurnoverBand> proposedTurnoverBands;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            this.account1 = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "one"))
                                {
                                    DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1"
                                };
            this.account2 = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 2, "two"))
                                {
                                    DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1"
                                };
            this.account3 = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 3, "three"))
                                {
                                    DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1"
                                };
            this.account4 = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 4, "four"))
                                {
                                    DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1"
                                };

            this.proposedTurnoverBands = new List<ProposedTurnoverBand>
                                            {
                                                new ProposedTurnoverBand
                                                    {
                                                        SalesAccount = this.account1,
                                                        FinancialYear = this.financialYear,
                                                        SalesValueCurrency = 1,
                                                        CalculatedTurnoverBandUri = "/tb/1",
                                                        ProposedTurnoverBandUri = "/tb/1",
                                                    },
                                                new ProposedTurnoverBand
                                                    {
                                                        SalesAccount = this.account2,
                                                        FinancialYear = this.financialYear,
                                                        SalesValueCurrency = 2,
                                                        CalculatedTurnoverBandUri = "/tb/2",
                                                        ProposedTurnoverBandUri = "/tb/2",
                                                    },
                                                new ProposedTurnoverBand
                                                    {
                                                        SalesAccount = this.account3,
                                                        FinancialYear = this.financialYear,
                                                        SalesValueCurrency = 3,
                                                        CalculatedTurnoverBandUri = "/tb/3",
                                                        ProposedTurnoverBandUri = "/tb/3",
                                                    }
                                            };
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear(this.financialYear)
                .Returns(this.proposedTurnoverBands);
            this.results = this.Sut.ApplyTurnoverBandProposal(this.financialYear, "/employees/100").ProposedTurnoverBands;
        }

        [Test]
        public void ShouldGetProposedTurnoverBands()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.financialYear);
        }

        [Test]
        public void ShouldUpdateAccounts()
        {
            this.account1.TurnoverBandUri.Should().Be("/tb/1");
            this.account2.TurnoverBandUri.Should().Be("/tb/2");
            this.account3.TurnoverBandUri.Should().Be("/tb/3");
            this.account1.Activities.Count(a => a is SalesAccountApplyTurnoverBandProposalActivity).Should().Be(1);
            this.account2.Activities.Count(a => a is SalesAccountApplyTurnoverBandProposalActivity).Should().Be(1);
            this.account3.Activities.Count(a => a is SalesAccountApplyTurnoverBandProposalActivity).Should().Be(1);
        }

        [Test]
        public void ShouldNotUpdateAccountsNotInProposal()
        {
            this.account4.TurnoverBandUri.Should().Be("/tb/1");
            this.account4.Activities.Count(a => a is SalesAccountApplyTurnoverBandProposalActivity).Should().Be(0);
        }

        [Test]
        public void ShouldMarkProposedTurnoverBandAsApplied()
        {
            this.proposedTurnoverBands.Count.Should().Be(3);
            this.proposedTurnoverBands.All(b => b.AppliedToAccount).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnTurnoverBands()
        {
            this.results.Count().Should().Be(3);
            this.results.All(b => b.AppliedToAccount).Should().BeTrue();
        }
    }
}
