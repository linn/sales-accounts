namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingSalesAccountsByNameAndAccountId : ContextBase
    {
        private SalesAccount salesAccount1;
        private SalesAccount salesAccount2;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount1 = new SalesAccount(new SalesAccountCreateActivity(123, "account 1")) { Id = 1 };
            this.salesAccount2 = new SalesAccount(new SalesAccountCreateActivity(456, "123rd Street")) { Id = 2 };
            this.SalesAccountRepository.GetById(123).Returns(this.salesAccount1);
            this.SalesAccountRepository.Get("123").Returns(new[] { this.salesAccount2 });
            this.Results = this.Sut.Get("123");
        }

        [Test]
        public void ShouldGetSalesAccounts()
        {
            this.SalesAccountRepository.Received().GetById(123);
            this.SalesAccountRepository.Received().Get("123");
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Results.Should().BeOfType<SuccessResult<IEnumerable<SalesAccount>>>();
            var dataResults = ((SuccessResult<IEnumerable<SalesAccount>>)this.Results).Data.ToList();
            dataResults.Should().HaveCount(2);
            dataResults.Should().Contain(a => a.Id == 1);
            dataResults.Should().Contain(a => a.Id == 2);
        }
    }
}
