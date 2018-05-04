namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{

    using FluentAssertions;

    using NUnit.Framework;

    public class WhenApplyingToAccount : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.ApplyTurnoverBandToAccount();
        }

        [Test]
        public void ShouldUpdateApplied()
        {
            this.Sut.AppliedToAccount.Should().BeTrue();
        }

        [Test]
        public void ShouldLeadToAccountBeingUpdated()
        {
            this.Sut.SalesAccount.TurnoverBandUri.Should().Be("/tb/1");
        }
    }
}
