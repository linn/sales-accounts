namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Exceptions;

    using NUnit.Framework;

    public class WhenApplyingTurnoverBandsWithNoYearSpecified : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.action = () => this.Sut.ApplyTurnoverBandProposal(null, "/employees/100");
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.ShouldThrow<NoFinancialYearSpecifiedException>();
        }
    }
}
