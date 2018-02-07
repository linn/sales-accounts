namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Dispatchers.Messages;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountName : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Result = this.Sut.UpdateSalesAccountName(1, "New Name");
        }

        [Test]
        public void ShouldGetFromRepository()
        {
            this.SalesAccountRepository.Received().GetById(1);
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldSendUpdatedMessage()
        {
            this.SalesAccountUpdatedDispatcher.Received()
                .SendSalesAccountUpdated(Arg.Is<SalesAccountMessage>(m => m.Id == 1));
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Result.Should().BeOfType<SuccessResult<SalesAccount>>();
            var dataResult = ((SuccessResult<SalesAccount>)this.Result).Data;
            dataResult.Name.Should().Be("New Name");
        }
    }
}
