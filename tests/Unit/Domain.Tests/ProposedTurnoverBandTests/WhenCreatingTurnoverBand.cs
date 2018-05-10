namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenCreatingTurnoverBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut = new ProposedTurnoverBand();
        }

        [Test]
        public void ShouldBeIncludedInUpdateByDefault()
        {
            this.Sut.IncludeInUpdate.Should().BeTrue();
        }
    }
}
