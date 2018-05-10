namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenExcludingTurnoverBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.ExcludeFromUpdate();
        }

        [Test]
        public void ShouldExcludeProposedBand()
        {
            this.Sut.IncludeInUpdate.Should().BeFalse();
        }
    }
}
