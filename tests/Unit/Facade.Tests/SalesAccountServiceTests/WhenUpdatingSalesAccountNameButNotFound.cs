namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountNameButNotFound : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.SalesAccountRepository.GetById(2).Returns((SalesAccount)null);
            this.Result = this.Sut.UpdateSalesAccountNameAndAddress(2, "Test", null, "/employees/100");
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
