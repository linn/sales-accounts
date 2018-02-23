namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountWithNoAddress : ContextBase
    {

        [SetUp]
        public void SetUp()
        {
            this.Result = this.Sut.UpdateSalesAccountNameAndAddress(1, "name", null);
        }

        [Test]
        public void ShouldTryToGetFromRepository()
        {
            this.SalesAccountRepository.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnBadRequestResult()
        {
            this.Result.Should().BeOfType<BadRequestResult<SalesAccount>>();
        }
    }
}
