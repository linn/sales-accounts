namespace Linn.SalesAccounts.Proxy.Tests.DiscountingServiceTests
{
    using FluentAssertions;

    using Linn.SalesAccounts.Domain.External;

    using NUnit.Framework;

    public class WhenGettingEmptyTurnoverBand : ContextBase
    {
        private TurnoverBand result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.GetTurnoverBand(null);
        }

        [Test]
        public void ShouldReturnNothing()
        {
            this.result.Should().BeNull();
        }
    }
}