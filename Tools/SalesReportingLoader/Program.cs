namespace Linn.SalesAccounts.SalesReportingLoader
{
    using Linn.Common.Configuration;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Common.Proxy;
    using Linn.SalesAccounts.Persistence;
    using Linn.SalesAccounts.Persistence.Repositories;

    public class Program
    {
        public static void Main(string[] args)
        {
            var proxyRoot = ConfigurationManager.Configuration["PROXY_ROOT"];
            var restClient = new RestClient();
            var serviceDbContext = new ServiceDbContext();
            var salesAccountsRepository = new SalesAccountRepository(serviceDbContext);
            var transactionManager = new TransactionManager(serviceDbContext);

            var growthPartnerLoader = new GrowthPartnerLoader(
                restClient,
                proxyRoot,
                transactionManager,
                salesAccountsRepository);

            growthPartnerLoader.Load();
        }
    }
}
