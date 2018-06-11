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

    public class WhenSearchingSalesAccountsByName : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "search something")) { Id = 1 };
            this.SalesAccountRepository.Get("search").Returns(new[] { this.salesAccount });
            this.Results = this.Sut.Get("search");
        }

        [Test]
        public void ShouldGetSalesAccount()
        {
            this.SalesAccountRepository.Received().Get("search");
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
