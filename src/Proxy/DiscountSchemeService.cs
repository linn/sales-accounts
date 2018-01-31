namespace Linn.SalesAccounts.Proxy
{
    using Linn.Common.Proxy;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Domain.Services;

    public class DiscountSchemeService : IDiscountSchemeService
    {
        private readonly IRestClient restClient;

        public DiscountSchemeService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public DiscountScheme GetDiscountScheme(string discountSchemeUri)
        {
            throw new System.NotImplementedException();
        }
    }
}