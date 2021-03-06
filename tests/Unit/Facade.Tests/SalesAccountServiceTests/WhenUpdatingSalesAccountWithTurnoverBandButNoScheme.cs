﻿namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountWithTurnoverBandButNoScheme : ContextBase
    {
        private SalesAccountUpdateResource updateResource;

        [SetUp]
        public void SetUp()
        {
            this.DiscountingService.GetDiscountScheme("/ds/1")
                .Returns((DiscountScheme)null);
            this.updateResource = new SalesAccountUpdateResource
                                      {
                                          DiscountSchemeUri = null,
                                          TurnoverBandUri = "/tb/1",
                                          EligibleForGoodCreditDiscount = true
                                      };
            this.Result = this.Sut.UpdateSalesAccount(1, this.updateResource, "/employees/100");
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
