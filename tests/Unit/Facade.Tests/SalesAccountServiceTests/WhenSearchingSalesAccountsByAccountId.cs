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

    public class WhenSearchingSalesAccountsByAccountId : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity(123, "search something")) { Id = 1 };
            this.SalesAccountRepository.GetById(123).Returns(this.salesAccount);
            this.Results = this.Sut.Get("123");
        }

        [Test]
        public void ShouldGetSalesAccount()
        {
            this.SalesAccountRepository.Received().GetById(123);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Results.Should().BeOfType<SuccessResult<IEnumerable<SalesAccount>>>();
            var dataResults = ((SuccessResult<IEnumerable<SalesAccount>>)this.Results).Data.ToList();
            dataResults.Should().HaveCount(1);
            dataResults.First().Id.Should().Be(1);
        }
    }
}
