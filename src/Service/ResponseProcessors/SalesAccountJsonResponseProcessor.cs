namespace Linn.SalesAccounts.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.SalesAccounts.Domain;

    public class SalesAccountJsonResponseProcessor : JsonResponseProcessor<SalesAccount>
    {
        public SalesAccountJsonResponseProcessor(IResourceBuilder<SalesAccount> resourceBuilder)
            : base(resourceBuilder, "sales.account", 1)
        {
        }
    }
}