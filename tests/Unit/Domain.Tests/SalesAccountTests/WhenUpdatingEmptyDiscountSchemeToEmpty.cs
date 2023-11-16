namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenUpdatingEmptyDiscountSchemeToEmpty : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.UpdateAccount(
                "/employees/100",
                null,
                null,
                this.Sut.EligibleForGoodCreditDiscount,
                this.Sut.EligibleForRebate,
                this.Sut.GrowthPartner,
                this.Sut.OnBoardingAccount);
        }

        [Test]
        public void ShouldNotUpdateAccount()
        {
            this.Sut.DiscountSchemeUri.Should().BeNull();
            this.Sut.TurnoverBandUri.Should().BeNull();
        }

        [Test]
        public void ShouldAddNoActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(0);
        }
    }
}
