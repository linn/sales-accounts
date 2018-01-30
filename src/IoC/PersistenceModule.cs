namespace Linn.SalesAccounts.IoC
{
    using Autofac;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Persistence;
    using Linn.SalesAccounts.Persistence.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceDbContext>().SingleInstance();
            builder.RegisterType<ServiceDbContext>().As<DbContext>();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();

            // repositories
            builder.RegisterType<SalesAccountRepository>().As<ISalesAccountRepository>();
        }
    }
}