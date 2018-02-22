namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;
    using Linn.SalesAccounts.Facade.Extensions;

    public class SalesAccountResourceBuilder : IResourceBuilder<SalesAccount>
    {
        public SalesAccountResource Build(SalesAccount salesAccount)
        {
            return new SalesAccountResource
            {
                Name = salesAccount.Name,
                Id = salesAccount.Id,
                EligibleForGoodCreditDiscount = salesAccount.EligibleForGoodCreditDiscount,
                EligibleForRebate = salesAccount.EligibleForRebate,
                GrowthPartner = salesAccount.GrowthPartner,
                DiscountSchemeUri = salesAccount.DiscountSchemeUri,
                TurnoverBandUri = salesAccount.TurnoverBandUri,
                Address = salesAccount.Address?.ToResource(),
                ClosedOn = salesAccount.ClosedOn == null ? string.Empty : salesAccount.ClosedOn.Value.ToString("o"),
                Links = this.BuildLinks(salesAccount).ToArray()
            };
        }

        object IResourceBuilder<SalesAccount>.Build(SalesAccount salesAccount) => this.Build(salesAccount);

        public string GetLocation(SalesAccount salesAccount) => $"/sales/accounts/{salesAccount.Id}";

        private IEnumerable<LinkResource> BuildLinks(SalesAccount salesAccount)
        {
            yield return new LinkResource("self", this.GetLocation(salesAccount));
        }
    }
}