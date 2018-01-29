namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSalesAccount : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity(1, 1, "name") { Id = 1 });
            this.SalesAccountRepository.GetById(1).Returns(this.salesAccount);
            this.Result = this.Sut.GetSalesAccount(1);
        }

        [Test]
        public void ShouldGetSalesAccount()
        {
            this.SalesAccountRepository.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Result.Should().BeOfType<SuccessResult<SalesAccount>>();
            var dataResult = ((SuccessResult<SalesAccount>)this.Result).Data;
            dataResult.Name.Should().Be(this.salesAccount.Name);
            dataResult.Id.Should().Be(this.salesAccount.Id);
            dataResult.AccountId.Should().Be(this.salesAccount.AccountId);
            dataResult.OutletNumber.Should().Be(this.salesAccount.OutletNumber);
        }
    }
}
