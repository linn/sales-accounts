namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private string updatedByUri; 

        [SetUp]
        public void SetUp()
        {
            this.updatedByUri = "/employees/100";
            var discountScheme = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/1" } };
            this.Sut.UpdateAccount(this.updatedByUri, discountScheme, "/tb/1", true, true, true);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.DiscountSchemeUri.Should().Be("/ds/1");
            this.Sut.TurnoverBandUri.Should().Be("/tb/1");
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
                .First(a => a is SalesAccountUpdateDiscountSchemeUriActivity)
                .As<SalesAccountUpdateDiscountSchemeUriActivity>().UpdatedByUri.Should().Be(this.updatedByUri);

            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateTurnoverBandUriActivity)
                .As<SalesAccountUpdateTurnoverBandUriActivity>().TurnoverBandUri.Should().Be("/tb/1");
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateTurnoverBandUriActivity)
                .As<SalesAccountUpdateTurnoverBandUriActivity>().UpdatedByUri.Should().Be(this.updatedByUri);

            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateGoodCreditActivity)
                .As<SalesAccountUpdateGoodCreditActivity>().EligibleForGoodCreditDiscount.Should().BeTrue();
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateGoodCreditActivity)
                .As<SalesAccountUpdateGoodCreditActivity>().UpdatedByUri.Should().Be(this.updatedByUri);

            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateRebateActivity)
                .As<SalesAccountUpdateRebateActivity>().EligibleForRebate.Should().BeTrue();
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateRebateActivity)
                .As<SalesAccountUpdateRebateActivity>().UpdatedByUri.Should().Be(this.updatedByUri);

            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountGrowthPartnerActivity)
                .As<SalesAccountGrowthPartnerActivity>().GrowthPartner.Should().BeTrue();
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountGrowthPartnerActivity)
                .As<SalesAccountGrowthPartnerActivity>().UpdatedByUri.Should().Be(this.updatedByUri);
        }
    }
}
