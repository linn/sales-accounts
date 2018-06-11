namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountButNotFound : ContextBase
    {
        private SalesAccountUpdateResource updateResource;

        [SetUp]
        public void SetUp()
        {
            this.updateResource = new SalesAccountUpdateResource();
            this.SalesAccountRepository.GetById(2).Returns((SalesAccount)null);
            this.Result = this.Sut.UpdateSalesAccount(2, this.updateResource, "/employees/100");
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
