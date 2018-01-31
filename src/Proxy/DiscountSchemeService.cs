﻿namespace Linn.SalesAccounts.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using Linn.Common.Proxy;
    using Linn.Common.Resources;
    using Linn.Common.Serialization.Json;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Proxy.Exceptions;
    using Linn.SalesAccounts.Resources.External;

    public class DiscountSchemeService : IDiscountSchemeService
    {
        private readonly IRestClient restClient;

        private readonly string proxyRoot;

        public DiscountSchemeService(IRestClient restClient, string proxyRoot)
        {
            this.restClient = restClient;
            this.proxyRoot = proxyRoot;
        }

        public DiscountScheme GetDiscountScheme(string discountSchemeUri)
        {
            var uri = new Uri($"{this.proxyRoot}{discountSchemeUri}", UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonHeaders()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProxyException("Error retrieving discount scheme.");
            }

            var json = new JsonSerializer();
            var discountScheme = json.Deserialize<DiscountSchemeResource>(response.Value);

            var turnoverBandSetUri = Relation.First(discountScheme.Links, "turnover-band-set");

            var turnoverBandResource = this.GeTurnoverBandSet(turnoverBandSetUri.ToString());

            return new DiscountScheme
                       {
                           DiscountSchemeUri = discountSchemeUri,
                           Name = discountScheme.Name,
                           TurnoverBandSetUri = turnoverBandSetUri.ToString(),
                           TurnoverBandUris = turnoverBandResource.TurnoverBands.Select(a => Relation.First(a.Links, "self").ToString())
                       };
        }

        private TurnoverBandSetResource GeTurnoverBandSet(string turnoverBandSetUri)
        {
            var uri = new Uri($"{this.proxyRoot}{turnoverBandSetUri}", UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonHeaders()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProxyException("Error retrieving turnover band set.");
            }

            var json = new JsonSerializer();
            var turnoverBandSetResource = json.Deserialize<TurnoverBandSetResource>(response.Value);

            return turnoverBandSetResource;
        }
    }
}