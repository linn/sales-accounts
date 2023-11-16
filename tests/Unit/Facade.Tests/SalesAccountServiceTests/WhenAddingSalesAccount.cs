namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingSalesAccount : ContextBase
    {
        private SalesAccountCreateResource createResource;

        [SetUp]
        public void SetUp()
        {
            this.createResource = new SalesAccountCreateResource { AccountId = 1, Name = "name" };
            this.Result = this.Sut.AddSalesAccount(this.createResource, "/employees/100");
        }

        [Test]
        public void ShouldAddToRepository()
        {
            this.SalesAccountRepository.Received().Add(Arg.Any<SalesAccount>());
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldReturnCreatedResult()
        {
            this.Result.Should().BeOfType<CreatedResult<SalesAccount>>();
            var dataResult = ((CreatedResult<SalesAccount>)this.Result).Data;
            dataResult.Id.Should().Be(1);
            dataResult.Name.Should().Be("name");
            dataResult.OnBoardingAccount.Should().BeFalse();
            dataResult.ClosedOn.Should().BeNull();
        }
    }
}
