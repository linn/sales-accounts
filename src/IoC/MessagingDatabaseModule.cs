namespace Linn.SalesAccounts.IoC
{
    using Autofac;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.SalesAccounts.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class MessagingDatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceDbContext>().AsSelf().As<DbContext>().InstancePerMatchingLifetimeScope("messageHandler");
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();
        }
    }
}