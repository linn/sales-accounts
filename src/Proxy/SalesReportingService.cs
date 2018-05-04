namespace Linn.SalesAccounts.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;

    using Linn.Common.Proxy;
    using Linn.Common.Serialization.Json;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Proxy.Exceptions;

    public class SalesReportingService : ISalesReportingService
    {
        private readonly IRestClient restClient;

        private readonly string proxyRoot;

        public SalesReportingService(IRestClient restClient, string proxyRoot)
        {
            this.restClient = restClient;
            this.proxyRoot = proxyRoot;
        }

        public IEnumerable<SalesDataDetail> GetSalesByAccount(string financialYear)
        {
            var year = string.IsNullOrEmpty(financialYear) ? "2017/18" : financialYear;
            var uri = new Uri(
                $"{this.proxyRoot}/sales/reporting/query?ReportOn=sales&companyCode=LINN&financialYear={year}&groupBy=sales-account",
                UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProxyException("Error retrieving sales data.");
            }

            var json = new JsonSerializer();
            return json.Deserialize<IEnumerable<SalesDataDetail>>(response.Value);
        }
    }
}