namespace Linn.SalesAccounts.IoC
{
    using Autofac;

    using Linn.Common.Configuration;
    using Linn.Common.Proxy;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Proxy;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<ProposedTurnoverBandService>().As<IProposedTurnoverBandService>();

            // facade services
            builder.RegisterType<SalesAccountService>().As<ISalesAccountService>();
            builder.RegisterType<TurnoverBandService>().As<ITurnoverBandService>();

            // proxies
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<DiscountSchemeService>().As<IDiscountSchemeService>().WithParameter("proxyRoot", ConfigurationManager.Configuration["PROXY_ROOT"]);
            builder.RegisterType<SalesReportingService>().As<ISalesReportingService>().WithParameter("proxyRoot", ConfigurationManager.Configuration["PROXY_ROOT"]);
        }
    }
}
