namespace Linn.SalesAccounts.IoC
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.SalesAccounts.Domain.Dispatchers;
    using Linn.SalesAccounts.Messaging.Dispatchers;

    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConnectionBuilder();
            builder.RegisterInfiniteRetryStrategy();
            builder.RegisterConnector();
            builder.RegisterMessageDispatcher();
            builder.RegisterSender("sales-accounts.x", "Sales Accounts Message Queuer");
            builder.RegisterConfiguration();
            builder.RegisterTerminator();

            builder.RegisterType<SalesAccountUpdateDispatcher>().As<ISalesAccountUpdatedDispatcher>();
        }
    }
}
