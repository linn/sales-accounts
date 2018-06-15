namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSalesAccountActivities : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "name"));
            this.salesAccount.UpdateGrowthPartner(new SalesAccountGrowthPartnerActivity("/employees/100", true));
            this.salesAccount.CloseAccount(new SalesAccountCloseActivity("/employees/100", 16.April(2018)));
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
            dataResult.Activities.Count.Should().Be(3);
            dataResult.Activities.First().GetType().Name.Should().Be("SalesAccountCreateActivity");
            dataResult.Activities.ElementAt(1).GetType().Name.Should().Be("SalesAccountGrowthPartnerActivity");
            dataResult.Activities.Last().GetType().Name.Should().Be("SalesAccountCloseActivity");
        }
    }
}
