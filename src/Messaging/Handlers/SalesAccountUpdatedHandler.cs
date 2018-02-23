namespace Linn.SalesAccounts.Messaging.Handlers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Resources.Messaging;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Newtonsoft.Json;

    public class SalesAccountUpdatedHandler
    {
        private readonly ISalesAccountService salesAccountService;

        private readonly ITransactionManager transactionManager;

        private readonly IRabbitTerminator rabbitTerminator;

        public SalesAccountUpdatedHandler(ISalesAccountService salesAccountService, ITransactionManager transactionManager, IRabbitTerminator rabbitTerminator)
        {
            this.salesAccountService = salesAccountService;
            this.transactionManager = transactionManager;
            this.rabbitTerminator = rabbitTerminator;
        }

        public bool Execute(IReceivedMessage message)
        {
            var content = Encoding.UTF8.GetString(message.Body);
            var resource = JsonConvert.DeserializeObject<LinnappsSalesAccountResource>(content);

            this.salesAccountService.UpdateSalesAccountNameAndAddress(resource.AccountId, resource.AccountName, resource.AccountAddress);

            if (!string.IsNullOrEmpty(resource.DateClosed))
            {
                this.salesAccountService.CloseSalesAccount(resource.AccountId, new SalesAccountCloseResource { ClosedOn = resource.DateClosed });
            }

            this.rabbitTerminator.Close();

            this.transactionManager.Commit();

            return true;
        }
    }
}