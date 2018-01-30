namespace Linn.SalesAccounts.IoC
{
    using Autofac;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Facade.ResourceBuilders;
    using Linn.SalesAccounts.Facade.Services;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // facade services
            builder.RegisterType<SalesAccountService>().As<ISalesAccountService>();

            // resource builders
            builder.RegisterType<SalesAccountResourceBuilder>().As<IResourceBuilder<SalesAccount>>();
        }
    }
}
