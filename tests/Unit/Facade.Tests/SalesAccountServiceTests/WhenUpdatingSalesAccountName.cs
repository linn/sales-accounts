namespace Linn.SalesAccounts.Facade.Tests.SalesAccountServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Dispatchers.Messages;
    using Linn.SalesAccounts.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountName : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var addressResource = new AddressResource
            {
                Line1 = "Address line 1",
                CountryUri = "/countries/1"
            };

            this.Result = this.Sut.UpdateSalesAccountNameAndAddress(1, "New Name", addressResource, "/employees/100");
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
            dataResult.Address.Line1.Should().Be("Address line 1");
        }
    }
}
