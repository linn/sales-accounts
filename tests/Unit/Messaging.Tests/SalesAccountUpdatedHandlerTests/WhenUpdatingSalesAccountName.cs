namespace Linn.SalesAccounts.Messaging.Tests.SalesAccountUpdatedHandlerTests
{
    using System.Text;

    using FluentAssertions;

    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.SalesAccounts.Resources.Messaging;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccountName : ContextBase
    {
        private bool result;
        private AddressResource addressResource;

        [SetUp]
        public void SetUp()
        {
            this.addressResource = new AddressResource
            {
                Line1 = "Address line 1",
                CountryUri = "/countries/1"
            };

            var resource = new LinnappsSalesAccountResource
            {
                AccountId = 1,
                AccountName = "New Name",
                AccountAddress = this.addressResource
            };

            var json = JsonConvert.SerializeObject(
                resource,
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            var body = Encoding.UTF8.GetBytes(json);

            var message = Substitute.For<IReceivedMessage>();
            message.Body.Returns(body);
            this.result = this.Sut.Execute(message);
        }

        [Test]
        public void ShouldCallFacadeToUpdateName()
        {
            this.SalesAccountService.Received(1).UpdateSalesAccountNameAndAddress(1, "New Name", Arg.Is<AddressResource>(a => a.Line1 == "Address line 1"));
        }

        [Test]
        public void ShouldNotCallFacadeToCloseAccount()
        {
            this.SalesAccountService.DidNotReceiveWithAnyArgs().CloseSalesAccount(1, Arg.Any<SalesAccountCloseResource>());
        }

        [Test]
        public void ShouldTerminateRabbitConnection()
        {
            this.RabbitTerminator.Received(1).Close();
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received(1).Commit();
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.result.Should().BeTrue();
        }
    }
}