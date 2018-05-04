namespace Linn.SalesAccounts.Messaging.Dispatchers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.SalesAccounts.Domain.Dispatchers;
    using Linn.SalesAccounts.Domain.Dispatchers.Messages;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class SalesAccountUpdateDispatcher : ISalesAccountUpdatedDispatcher
    {
        private const string ContentType = "application/json";

        private const string RoutingKey = "sales.account.updated";

        private readonly IMessageDispatcher messageDispatcher;

        public SalesAccountUpdateDispatcher(IMessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }

        public void SendSalesAccountUpdated(SalesAccountMessage message)
        {
            var json = JsonConvert.SerializeObject(
                message,
                new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

            var body = Encoding.UTF8.GetBytes(json);

            this.messageDispatcher.Dispatch(RoutingKey, body, ContentType);
        }
    }
}
