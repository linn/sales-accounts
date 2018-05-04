namespace Linn.SalesAccounts.Proxy.Tests.SalesReportingServiceTests
{
    using System.Collections.Generic;

    using Linn.Common.Proxy;
    using Linn.SalesAccounts.Domain.External;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected SalesReportingService Sut { get; private set; }

        protected IRestClient RestClient { get; private set; }

        protected string ProxyRoot { get; private set; }

        protected IEnumerable<SalesDataDetail> Results { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ProxyRoot = "http://app.linn.co.uk";
            this.RestClient = Substitute.For<IRestClient>();
            this.Sut = new SalesReportingService(this.RestClient, this.ProxyRoot);
        }
    }
}
