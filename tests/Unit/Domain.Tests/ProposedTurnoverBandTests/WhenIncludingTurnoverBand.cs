namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenIncludingTurnoverBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.ExcludeFromUpdate();
            this.Sut.IncludeProposalInUpdate();
        }

        [Test]
        public void ShouldIncludeProposedBand()
        {
            this.Sut.IncludeInUpdate.Should().BeTrue();
        }
    }
}
