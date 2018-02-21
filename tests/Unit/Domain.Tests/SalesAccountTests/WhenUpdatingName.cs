namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenUpdatingName : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var address = new SalesAccountAddress("Address line 1", "Address line 2", "Address line 3", "Address line 4", "/countries/1", "Postcode");
            this.Sut.UpdateNameAndAddress("New Name", address);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.Name.Should().Be("New Name");
            this.Sut.Address.Line1.Should().Be("Address line 1");
            this.Sut.Address.CountryUri.Should().Be("/countries/1");
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(2);
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateNameActivity)
                .As<SalesAccountUpdateNameActivity>().Name.Should().Be("New Name");
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateAddressActivity)
                .As<SalesAccountUpdateAddressActivity>().Address.Line1.Should().Be("Address line 1");
        }
    }
}
