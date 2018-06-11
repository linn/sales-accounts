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

    public class WhenSearchingForAllSalesAccounts : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 123, "account")) { Id = 1 };
            this.SalesAccountRepository.GetAllOpenAccounts().Returns(new[] { this.salesAccount });
            this.Results = this.Sut.Get(string.Empty);
        }

        [Test]
        public void ShouldGetSalesAccounts()
        {
            this.SalesAccountRepository.Received().GetAllOpenAccounts();
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
