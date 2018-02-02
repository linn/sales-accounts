namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Dispatchers.Messages;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccount : ContextBase
    {
        private SalesAccountUpdateResource updateResource;

        [SetUp]
        public void SetUp()
        {
            this.DiscountSchemeService.GetDiscountScheme("/ds/1")
                .Returns(new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/1" } });
            this.updateResource = new SalesAccountUpdateResource
                                      {
                                          DiscountSchemeUri = "/ds/1",
                                          TurnoverBandUri = "/tb/1",
                                          EligibleForGoodCreditDiscount = true
                                      };
            this.Result = this.Sut.UpdateSalesAccount(1, this.updateResource);
        }

        [Test]
        public void ShouldGetFromRepository()
        {
            this.SalesAccountRepository.Received().GetById(1);
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldSendUpdatedMessage()
        {
            this.SalesAccountUpdatedDispatcher.Received()
                .SendSalesAccountUpdated(Arg.Is<SalesAccountMessage>(m => m.Id == 1));
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Result.Should().BeOfType<SuccessResult<SalesAccount>>();
            var dataResult = ((SuccessResult<SalesAccount>)this.Result).Data;
            dataResult.DiscountSchemeUri.Should().Be("/ds/1");
            dataResult.TurnoverBandUri.Should().Be("/tb/1");
            dataResult.EligibleForGoodCreditDiscount.Should().BeTrue();
        }
    }
}
