namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenUpdatingAddressWithoutAddress : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.UpdateNameAndAddress("/employees/100", "Account 10", null);
        }

        [Test]
        public void ShouldNotAddAddress()
        {
            this.Sut.Address.Should().Be(null);
        }

        [Test]
        public void ShouldNotAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(0);
        }
    }
}
