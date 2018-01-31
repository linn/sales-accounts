namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var discountScheme = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/1" } };
            this.Sut.UpdateAccount(discountScheme, "/tb/1", true, true);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.DiscountSchemeUri.Should().Be("/ds/1");
            this.Sut.TurnoverBandUri.Should().Be("/tb/1");
            this.Sut.EligibleForGoodCreditDiscount.Should().BeTrue();
            this.Sut.EligibleForRebate.Should().BeTrue();
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(4);
            ((SalesAccountUpdateDiscountSchemeUriActivity)this.ActivitiesExcludingCreate()
                .First(a => a.GetType() == typeof(SalesAccountUpdateDiscountSchemeUriActivity))).DiscountSchemeUri
                .Should().Be("/ds/1");
            ((SalesAccountUpdateTurnoverBandUriActivity)this.ActivitiesExcludingCreate()
                .First(a => a.GetType() == typeof(SalesAccountUpdateTurnoverBandUriActivity))).TurnoverBandUri
                .Should().Be("/tb/1");
            ((SalesAccountUpdateGoodCreditActivity)this.ActivitiesExcludingCreate()
                .First(a => a.GetType() == typeof(SalesAccountUpdateGoodCreditActivity))).EligibleForGoodCreditDiscount
                .Should().BeTrue();
            ((SalesAccountUpdateRebateActivity)this.ActivitiesExcludingCreate()
                    .First(a => a.GetType() == typeof(SalesAccountUpdateRebateActivity))).EligibleForRebate
                .Should().BeTrue();
        }
    }
}
