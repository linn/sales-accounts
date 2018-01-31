namespace Linn.SalesAccounts.Domain.External
{
    using System.Collections.Generic;

    public class DiscountScheme
    {
        public string DiscountSchemeUri { get; set; }

        public string Name { get; set; }

        public string TurnoverBandSetUri { get; set; }

        public IEnumerable<string> TurnoverBandUris { get; set; }
    }
}
