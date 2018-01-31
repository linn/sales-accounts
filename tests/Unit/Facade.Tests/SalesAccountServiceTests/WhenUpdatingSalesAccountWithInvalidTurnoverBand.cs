namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountWithInvalidTurnoverBand : ContextBase
    {
        private SalesAccountUpdateResource updateResource;

        [SetUp]
        public void SetUp()
        {
            this.DiscountSchemeService.GetDiscountScheme("/ds/1")
                .Returns(new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/2" } });
            this.updateResource = new SalesAccountUpdateResource
                                      {
                                          DiscountSchemeUri = "/ds/1",
                                          TurnoverBandUri = "/tb/1",
                                          EligibleForGoodCreditDiscount = true
                                      };
            this.Result = this.Sut.UpdateSalesAccount(1, this.updateResource);
        }

        [Test]
        public void ShouldNotCommitTransaction()
        {
            this.TransactionManager.DidNotReceive().Commit();
        }

        [Test]
        public void ShouldReturnBadRequestResult()
        {
            this.Result.Should().BeOfType<BadRequestResult<SalesAccount>>();
        }
    }
}
