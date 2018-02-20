namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Exceptions;
    using Linn.SalesAccounts.Domain.External;

    using NUnit.Framework;

    public class WhenUpdatingWithInvalidTurnoverBand : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            var discountScheme = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/2" } };
            this.action = () => this.Sut.UpdateAccount(discountScheme, "/tb/1", true, true, true, "Street, Town");
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.ShouldThrow<InvalidTurnoverBandException>();
        }
    }
}
