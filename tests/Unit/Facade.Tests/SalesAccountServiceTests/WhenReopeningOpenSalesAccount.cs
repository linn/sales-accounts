namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenReopeningOpenSalesAccount : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Result = this.Sut.ReopenSalesAccountIfClosed(1, "/employees/100");
        }

        [Test]
        public void ShouldGetFromRepository()
        {
            this.SalesAccountRepository.Received().GetById(1);
        }

        [Test]
        public void ShouldNotCommitTransaction()
        {
            this.TransactionManager.DidNotReceive().Commit();
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Result.Should().BeOfType<SuccessResult<SalesAccount>>();
            var dataResult = ((SuccessResult<SalesAccount>)this.Result).Data;
            dataResult.ClosedOn.Should().BeNull();
            dataResult.Activities.Where(a => a.GetType() == typeof(SalesAccountReopenActivity)).Should().HaveCount(0);
        }
    }
}
