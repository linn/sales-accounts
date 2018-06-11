namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenUpdatingAddress : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var address = new SalesAccountAddress(
                "Address line 1",
                "Address line 2",
                "Address line 3",
                "Address line 4",
                "/countries/1",
                "Postcode");
            this.Sut.UpdateNameAndAddress("/employees/100", "Account 10", address);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.Address.Line1.Should().Be("Address line 1");
            this.Sut.Address.CountryUri.Should().Be("/countries/1");
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(1);

            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateAddressActivity)
                .As<SalesAccountUpdateAddressActivity>().Address.Line1.Should().Be("Address line 1");
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateAddressActivity)
                .As<SalesAccountUpdateAddressActivity>().UpdatedByUri.Should().Be("/employees/100");
        }
    }
}
