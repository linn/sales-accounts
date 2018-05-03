namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{

    using FluentAssertions;

    using NUnit.Framework;

    public class WhenOverridingTurnoverBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.OverrideProposedTurnoverBand("/tb/100");
        }

        [Test]
        public void ShouldUpdateProposedBand()
        {
            this.Sut.ProposedTurnoverBandUri.Should().Be("/tb/100");
        }

        [Test]
        public void ShouldNotUpdateCalculatedBand()
        {
            this.Sut.CalculatedTurnoverBandUri.Should().Be("/tb/1");
        }
    }
}
