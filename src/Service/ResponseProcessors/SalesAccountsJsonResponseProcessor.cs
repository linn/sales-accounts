namespace Linn.SalesAccounts.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.SalesAccounts.Domain;

    public class SalesAccountsJsonResponseProcessor : JsonResponseProcessor<IEnumerable<SalesAccount>>
    {
        public SalesAccountsJsonResponseProcessor(IResourceBuilder<IEnumerable<SalesAccount>> resourceBuilder)
            : base(resourceBuilder, "sales.accounts", 1)
        {
        }
    }
}