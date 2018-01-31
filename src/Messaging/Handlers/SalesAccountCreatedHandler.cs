namespace Linn.SalesAccounts.Messaging.Handlers
{
    using System.Text;

    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Resources.Messaging;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Newtonsoft.Json;

    public class SalesAccountCreatedHandler
    {
        private readonly SalesAccountService salesAccountService;

        private readonly ITransactionManager transactionManager;

        public SalesAccountCreatedHandler(SalesAccountService salesAccountService, ITransactionManager transactionManager)
        {
            this.salesAccountService = salesAccountService;
            this.transactionManager = transactionManager;
        }

        public bool Execute(IReceivedMessage message)
        {
            var content = Encoding.UTF8.GetString(message.Body);
            var resource = JsonConvert.DeserializeObject<LinnappsSalesAccountResource>(content);
            this.salesAccountService.AddSalesAccount(new SalesAccountCreateResource { AccountId = resource.AccountId, Name = resource.AccountName });

            this.transactionManager.Commit();

            return true;
        }
    }
}