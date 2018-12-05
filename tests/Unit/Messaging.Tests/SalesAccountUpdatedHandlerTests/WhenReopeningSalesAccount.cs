namespace Linn.SalesAccounts.Messaging.Tests.SalesAccountUpdatedHandlerTests
{
    using System.Text;

    using FluentAssertions;

    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.SalesAccounts.Resources;
    using Linn.SalesAccounts.Resources.Messaging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenReopeningSalesAccount : ContextBase
    {
        private bool result;

        private LinnappsSalesAccountResource resource;
        private AddressResource addressResource;

        [SetUp]
        public void SetUp()
        {
            this.addressResource = new AddressResource
            {
                Line1 = "Address line 1",
                CountryUri = "/countries/1",
            };

            this.resource = new LinnappsSalesAccountResource
            {
                AccountId = 1,
                AccountName = "Name",
                AccountAddress = this.addressResource
            };

            var json = JsonConvert.SerializeObject(
                this.resource,
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
        public void ShouldCallFacadeToMaybeReopenAccount()
        {
            this.SalesAccountService.Received(1).ReopenSalesAccountIfClosed(1, "/employees/100");
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