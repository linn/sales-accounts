namespace Linn.SalesAccounts.Resources.External
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class TurnoverBandSetResource : HypermediaResource
    {
        public string Name { get; set; }

        public IEnumerable<TurnoverBandResource> TurnoverBands { get; set; }
    }
}