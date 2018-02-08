namespace Linn.SalesAccounts.IoC
{
    using Autofac;

    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Persistence.Repositories;

    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SalesAccountRepository>().As<ISalesAccountRepository>();
        }
    }
}