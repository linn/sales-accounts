namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenOverridingAppliedTurnoverBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.AppliedToAccount = true;
            this.Sut.OverrideProposedTurnoverBand("/tb/100");
        }

        [Test]
        public void ShouldNotUpdateProposedBand()
        {
            this.Sut.ProposedTurnoverBandUri.Should().Be("/tb/1");
        }
    }
}
