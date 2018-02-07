namespace Linn.SalesAccounts.SalesReportingLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using Linn.Common.Persistence.EntityFramework;
    using Linn.Common.Proxy;
    using Linn.Common.Serialization.Json;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Persistence.Repositories;
    using Linn.SalesAccounts.SalesReportingLoader.Resources;

    public class GrowthPartnerLoader
    {
        private readonly RestClient restClient;

        private readonly string proxyRoot;

        private readonly TransactionManager transactionManager;

        private readonly SalesAccountRepository salesAccountsRepository;

        public GrowthPartnerLoader(
            RestClient restClient,
            string proxyRoot,
            TransactionManager transactionManager,
            SalesAccountRepository salesAccountsRepository)
        {
            this.restClient = restClient;
            this.proxyRoot = proxyRoot;
            this.transactionManager = transactionManager;
            this.salesAccountsRepository = salesAccountsRepository;
        }

        public void Load()
        {
            var json = new JsonSerializer();
            var response = this.restClient.Get(
                CancellationToken.None,
                this.BuildUri("/sales/reporting/customers/gpa"),
                new Dictionary<string, string>(),
                this.BuildGetHeaders()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Could not get customers");
            }

            var customers = json.Deserialize<IEnumerable<CustomerDimensionResource>>(response.Value);
            var uniqueAccounts = customers.GroupBy(a => a.SalesAccountId).Select(c => c.First());
            foreach (var customerResource in uniqueAccounts)
            {
                var account = this.salesAccountsRepository.GetById(customerResource.SalesAccountId);
                account?.UpdateGrowthPartner(new SalesAccountGrowthPartnerActivity(customerResource.GrowthPartner));
            }

            this.transactionManager.Commit();
        }

        private Dictionary<string, string[]> BuildGetHeaders()
        {
            return new Dictionary<string, string[]> { { "Accept", new[] { "application/json" } } };
        }

        private Uri BuildUri(string uri)
        {
            return new Uri($"{this.proxyRoot}{uri}", UriKind.RelativeOrAbsolute);
        }
    }
}
