namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenExcludingAppliedTurnoverBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.AppliedToAccount = true;
            this.Sut.ExcludeFromUpdate();
        }

        [Test]
        public void ShouldNotUpdateProposedBand()
        {
            this.Sut.IncludeInUpdate.Should().BeTrue();
        }
    }
}
