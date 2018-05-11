namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenApplyingTurnoverBandsNotIncluded : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> results;

        private string financialYear;

        private SalesAccount account;

        private List<ProposedTurnoverBand> proposedTurnoverBands;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            this.account = new SalesAccount(new SalesAccountCreateActivity(1, "one")) { DiscountSchemeUri = "/ds/1", TurnoverBandUri = "/tb/1" };
            var band = new ProposedTurnoverBand
                           {
                               SalesAccount = this.account,
                               FinancialYear = this.financialYear,
                               SalesValueCurrency = 1,
                               CalculatedTurnoverBandUri = "/tb/2",
                               ProposedTurnoverBandUri = "/tb/2",
                           };
            band.ExcludeFromUpdate();
            this.proposedTurnoverBands = new List<ProposedTurnoverBand>
                                            {
                                                band
                                            };
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear(this.financialYear)
                .Returns(this.proposedTurnoverBands);
            this.results = this.Sut.ApplyTurnoverBandProposal(this.financialYear).ProposedTurnoverBands;
        }

        [Test]
        public void ShouldGetProposedTurnoverBands()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.financialYear);
        }

        [Test]
        public void ShouldNotUpdateAccount()
        {
            this.account.TurnoverBandUri.Should().Be("/tb/1");
            this.account.Activities.Count(a => a is SalesAccountApplyTurnoverBandProposalActivity).Should().Be(0);
        }

        [Test]
        public void ShouldNotMarkProposedTurnoverBandAsApplied()
        {
            this.proposedTurnoverBands.First().AppliedToAccount.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTurnoverBands()
        {
            this.results.Count().Should().Be(1);
            this.results.All(b => b.AppliedToAccount).Should().BeFalse();
        }
    }
}
