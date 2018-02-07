namespace Linn.SalesAccounts.Messaging.Tests.SalesAccountCreatedHandlerTests
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

    public class WhenCreatingSalesAccount : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            var resource = new LinnappsSalesAccountResource
                               {
                                   AccountId = 1,
                                   AccountName = "Some Account"
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
        public void ShouldCallFacadeToCreateAccount()
        {
            this.SalesAccountService.Received(1).AddSalesAccount(Arg.Any<SalesAccountCreateResource>());
        }

        [Test]
        public void ShouldTerminateRabbitConnection()
        {
            this.RabbitTerminator.Received(1).Close();
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.result.Should().BeTrue();
        }
    }
}