namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenApplyingTurnoverBandProposal : ContextBase
    {
        private ProposedTurnoverBand proposedTurnoverBand;

        [SetUp]
        public void SetUp()
        {
            this.proposedTurnoverBand = new ProposedTurnoverBand
                                            {
                                                AppliedToAccount = false,
                                                ProposedTurnoverBandUri = "/tb/888",
                                                SalesAccount = this.Sut,
                                                FinancialYear = "2018/19"
                                            };
            this.Sut.ApplyTurnoverBandProposal(this.proposedTurnoverBand);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.TurnoverBandUri.Should().Be(this.proposedTurnoverBand.ProposedTurnoverBandUri);
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(1);
            var activity = this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountApplyTurnoverBandProposalActivity)
                .As<SalesAccountApplyTurnoverBandProposalActivity>();
            activity.TurnoverBandUri.Should().Be(this.proposedTurnoverBand.ProposedTurnoverBandUri);
            activity.BasedOnFinancialYear.Should().Be(this.proposedTurnoverBand.FinancialYear);
        }
    }
}
