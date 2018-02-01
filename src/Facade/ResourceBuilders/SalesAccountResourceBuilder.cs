﻿namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class SalesAccountResourceBuilder : IResourceBuilder<SalesAccount>
    {
        public SalesAccountResource Build(SalesAccount salesAccount)
        {
            return new SalesAccountResource
            {
                Name = salesAccount.Name,
                AccountId = salesAccount.Id,
                EligibleForGoodCreditDiscount = salesAccount.EligibleForGoodCreditDiscount,
                DiscountSchemeUri = salesAccount.DiscountSchemeUri,
                TurnoverBandUri = salesAccount.TurnoverBandUri,
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