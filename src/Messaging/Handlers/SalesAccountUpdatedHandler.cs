namespace Linn.SalesAccounts.Messaging.Handlers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Resources.Messaging;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Newtonsoft.Json;

    public class SalesAccountUpdatedHandler
    {
        private readonly SalesAccountService salesAccountService;

        private readonly ITransactionManager transactionManager;

        public SalesAccountUpdatedHandler(SalesAccountService salesAccountService, ITransactionManager transactionManager)
        {
            this.salesAccountService = salesAccountService;
            this.transactionManager = transactionManager;
        }

        public bool Execute(IReceivedMessage message)
        {
            var content = Encoding.UTF8.GetString(message.Body);
            var resource = JsonConvert.DeserializeObject<LinnappsSalesAccountResource>(content);

            this.salesAccountService.UpdateSalesAccountName(resource.AccountId, resource.AccountName);

            if (!string.IsNullOrEmpty(resource.DateClosed))
            {
                this.salesAccountService.CloseSalesAccount(resource.AccountId, new SalesAccountCloseResource { ClosedOn = resource.DateClosed });
            }

            this.transactionManager.Commit();

            return true;
        }
    }
}