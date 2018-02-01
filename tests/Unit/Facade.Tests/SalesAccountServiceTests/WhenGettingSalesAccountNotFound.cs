namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSalesAccountNotFound : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.SalesAccountRepository.GetById(1).Returns((SalesAccount)null);
            this.Result = this.Sut.GetById(1);
        }

        [Test]
        public void ShouldTryToGetSalesAccount()
        {
            this.SalesAccountRepository.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnNotFoundResult()
        {
            this.Result.Should().BeOfType<NotFoundResult<SalesAccount>>();
        }
    }
}
