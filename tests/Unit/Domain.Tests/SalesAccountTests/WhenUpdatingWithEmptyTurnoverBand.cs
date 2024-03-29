﻿namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;

    using NUnit.Framework;

    public class WhenUpdatingWithEmptyTurnoverBand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var discountScheme = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/1" } };
            this.Sut.UpdateAccount(
                "/employees/100",
                discountScheme,
                null,
                this.Sut.EligibleForGoodCreditDiscount,
                this.Sut.EligibleForRebate,
                this.Sut.GrowthPartner,
                this.Sut.OnBoardingAccount);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.DiscountSchemeUri.Should().Be("/ds/1");
            this.Sut.TurnoverBandUri.Should().BeNull();
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(1);
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateDiscountSchemeUriActivity)
                .As<SalesAccountUpdateDiscountSchemeUriActivity>().DiscountSchemeUri.Should().Be("/ds/1");
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateDiscountSchemeUriActivity)
                .As<SalesAccountUpdateDiscountSchemeUriActivity>().UpdatedByUri.Should().Be("/employees/100");
        }
    }
}
