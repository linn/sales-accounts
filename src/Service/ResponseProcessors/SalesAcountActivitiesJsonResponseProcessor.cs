namespace Linn.SalesAccounts.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    public class SalesAcountActivitiesJsonResponseProcessor : JsonResponseProcessor<IEnumerable<SalesAccountActivity>>
    {
        public SalesAcountActivitiesJsonResponseProcessor(
            IResourceBuilder<IEnumerable<SalesAccountActivity>> resourceBuilder)
            : base(resourceBuilder, "sales.account.activities", 1)
        {
        }
    }
}