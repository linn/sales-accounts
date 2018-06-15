namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenUpdatingGrowthPartner : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.UpdateGrowthPartner(new SalesAccountGrowthPartnerActivity("/employees/100", true));
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.GrowthPartner.Should().BeTrue();
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(1);
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountGrowthPartnerActivity)
                .As<SalesAccountGrowthPartnerActivity>().GrowthPartner.Should().BeTrue();
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountGrowthPartnerActivity)
                .As<SalesAccountGrowthPartnerActivity>().UpdatedByUri.Should().Be("/employees/100");
        }
    }
}
