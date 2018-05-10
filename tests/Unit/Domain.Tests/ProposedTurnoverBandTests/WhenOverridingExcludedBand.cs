namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenOverridingExcludedBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.IncludeInUpdate = false;
            this.Sut.OverrideProposedTurnoverBand("/tb/100");
        }

        [Test]
        public void ShouldReincludeProposal()
        {
            this.Sut.IncludeInUpdate.Should().BeTrue();
        }
    }
}
