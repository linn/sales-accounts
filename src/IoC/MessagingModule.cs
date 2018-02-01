namespace Linn.SalesAccounts.IoC
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ.Autofac;

    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConnectionBuilder();
            builder.RegisterInfiniteRetryStrategy();
            builder.RegisterConnector();
            builder.RegisterMessageDispatcher();
            builder.RegisterConfiguration();
            builder.RegisterTerminator();
        }
    }
}
