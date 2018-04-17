namespace Linn.SalesAccounts.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<SalesAccountResourceBuilder>().As<IResourceBuilder<SalesAccount>>();
            builder.RegisterType<SalesAccountsResourceBuilder>().As<IResourceBuilder<IEnumerable<SalesAccount>>>();
            builder.RegisterType<SalesAccountActivitiesResourceBuilder>().As<IResourceBuilder<IEnumerable<SalesAccountActivity>>>();
        }
    }
}
