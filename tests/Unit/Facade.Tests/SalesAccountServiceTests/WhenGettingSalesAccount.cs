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
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "name"));
            this.SalesAccountRepository.GetById(1).Returns(this.salesAccount);
            this.Result = this.Sut.GetById(1);
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
            dataResult.Id.Should().Be(1);
        }
    }
}
