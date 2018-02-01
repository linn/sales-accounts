namespace Linn.SalesAccounts.Proxy.Tests.DiscountSchemeServiceTests
{
    using Linn.Common.Proxy;
    using Linn.SalesAccounts.Domain.External;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected DiscountSchemeService Sut { get; private set; }

        protected DiscountScheme Result { get; set; }

        protected IRestClient RestClient { get; private set; }

        protected string ProxyRoot { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ProxyRoot = "http://app.linn.co.uk";
            this.RestClient = Substitute.For<IRestClient>();
            this.Sut = new DiscountSchemeService(this.RestClient, this.ProxyRoot);
        }
    }
}
