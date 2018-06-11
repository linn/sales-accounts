namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingSalesAccount : ContextBase
    {
        private SalesAccountCloseResource closeResource;

        [SetUp]
        public void SetUp()
        {
            this.closeResource = new SalesAccountCloseResource { ClosedOn = 1.December(2020).ToString("O") };
            this.Result = this.Sut.CloseSalesAccount(1, this.closeResource, "/employees/100");
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
        public void ShouldReturnSuccessResult()
        {
            this.Result.Should().BeOfType<SuccessResult<SalesAccount>>();
            var dataResult = ((SuccessResult<SalesAccount>)this.Result).Data;
            dataResult.ClosedOn.Should().Be(1.December(2020));
        }
    }
}
