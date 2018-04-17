namespace Linn.SalesAccounts.Resources.SalesAccounts
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class SalesAccountActivitiesResource : HypermediaResource
    {
        public IEnumerable<SalesAccountActivityResource> Activities { get; set; }
    }
}