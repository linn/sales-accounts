namespace Linn.SalesAccounts.Messaging.Host
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.SalesAccounts.IoC;
    using Linn.SalesAccounts.Messaging.Handlers;

    public static class Configuration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AmazonCredentialsModule>();
            builder.RegisterModule<AmazonSqsModule>();
            builder.RegisterModule<LoggingModule>();
            builder.RegisterModule<MessagingModule>();
            builder.RegisterModule<MessagingDatabaseModule>();
            builder.RegisterModule<PersistenceModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterReceiver("sales-accounts.q", "sales-accounts.dlx");

            builder.RegisterType<Listener>().AsSelf();
            builder.RegisterType<SalesAccountCreatedHandler>().AsSelf();
            builder.RegisterType<SalesAccountUpdatedHandler>().AsSelf();

            return builder.Build();
        }
    }
}
