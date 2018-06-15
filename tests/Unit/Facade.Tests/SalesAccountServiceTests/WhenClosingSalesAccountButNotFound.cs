namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingSalesAccountButNotFound : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.SalesAccountRepository.GetById(2).Returns((SalesAccount)null);
            this.Result = this.Sut.CloseSalesAccount(2, new SalesAccountCloseResource(), "/employees/100");
        }

        [Test]
        public void ShouldTryToGetFromRepository()
        {
            this.SalesAccountRepository.Received().GetById(2);
        }

        [Test]
        public void ShouldReturnNotFoundResult()
        {
            this.Result.Should().BeOfType<NotFoundResult<SalesAccount>>();
        }
    }
}
