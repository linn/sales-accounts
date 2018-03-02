namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;
    using Activities.SalesAccounts;
    using External;
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenUpdatingWithNoTurnoverbandSet : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var discountScheme = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/1" } };
            this.Sut.UpdateAccount(discountScheme, "", true, true, true);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.DiscountSchemeUri.Should().Be("/ds/1");
            this.Sut.TurnoverBandUri.Should().BeNullOrEmpty();
            this.Sut.EligibleForGoodCreditDiscount.Should().BeTrue();
            this.Sut.EligibleForRebate.Should().BeTrue();
            this.Sut.GrowthPartner.Should().BeTrue();
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(5);
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateDiscountSchemeUriActivity)
                .As<SalesAccountUpdateDiscountSchemeUriActivity>().DiscountSchemeUri.Should().Be("/ds/1");
             this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateGoodCreditActivity)
                .As<SalesAccountUpdateGoodCreditActivity>().EligibleForGoodCreditDiscount.Should().BeTrue();
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateRebateActivity)
                .As<SalesAccountUpdateRebateActivity>().EligibleForRebate.Should().BeTrue();
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountGrowthPartnerActivity)
                .As<SalesAccountGrowthPartnerActivity>().GrowthPartner.Should().BeTrue();
        }
    }
}