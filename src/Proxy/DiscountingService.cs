namespace Linn.SalesAccounts.Proxy
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

    public class DiscountingService : IDiscountingService
    {
        private readonly IRestClient restClient;

        private readonly string proxyRoot;

        public DiscountingService(IRestClient restClient, string proxyRoot)
        {
            this.restClient = restClient;
            this.proxyRoot = proxyRoot;
        }

        public DiscountScheme GetDiscountScheme(string discountSchemeUri)
        {
            if (string.IsNullOrEmpty(discountSchemeUri))
            {
                return null;
            }

            var uri = new Uri($"{this.proxyRoot}{discountSchemeUri}", UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProxyException("Error retrieving discount scheme.");
            }

            var json = new JsonSerializer();
            var discountScheme = json.Deserialize<DiscountSchemeResource>(response.Value);

            var turnoverBandSetUri = Relation.First(discountScheme.Links, "turnover-band-set");

            var turnoverBandResource = turnoverBandSetUri == null ? null : this.GetTurnoverBandSet(turnoverBandSetUri.ToString());

            return new DiscountScheme
                       {
                           DiscountSchemeUri = discountSchemeUri,
                           Name = discountScheme.Name,
                           TurnoverBandSetUri = turnoverBandSetUri == null ? string.Empty : turnoverBandSetUri.ToString(),
                           TurnoverBandUris = turnoverBandResource?.TurnoverBands.Select(a => Relation.First(a.Links, "self").ToString())
                       };
        }

        public string GetTurnoverBandForTurnoverValue(
            string turnoverBandSetUri,
            string currencyCode,
            decimal turnoverValue)
        {
            if (string.IsNullOrEmpty(turnoverBandSetUri))
            {
                return null;
            }

            var uri = new Uri(
                $"{this.proxyRoot}{turnoverBandSetUri}/turnover-bands?currencyUri=/currencies/{currencyCode}&turnoverValue={turnoverValue}",
                UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProxyException($"Error retrieving turnover band at {uri}. Status code {response.StatusCode}.");
            }

            var json = new JsonSerializer();
            var turnoverBand = json.Deserialize<TurnoverBandResource>(response.Value);


            return Relation.First(turnoverBand.Links, "self").ToString();
        }

        public TurnoverBand GetTurnoverBand(string turnoverBandUri)
        {
            var uri = new Uri($"{this.proxyRoot}{turnoverBandUri}", UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ProxyException("Error retrieving turnover band.");
            }

            var json = new JsonSerializer();
            var turnoverBandResource = json.Deserialize<TurnoverBandResource>(response.Value);
            var turnoverBand = new TurnoverBand
                                   {
                                       Name = turnoverBandResource.Name,
                                       TurnoverBandUri = Relation.First(turnoverBandResource.Links, "self").ToString()
                                   };

            return turnoverBand;
        }

        private TurnoverBandSetResource GetTurnoverBandSet(string turnoverBandSetUri)
        {
            var uri = new Uri($"{this.proxyRoot}{turnoverBandSetUri}", UriKind.RelativeOrAbsolute);

            var response = this.restClient.Get(
                CancellationToken.None,
                uri,
                new Dictionary<string, string>(),
                DefaultHeaders.JsonGetHeaders()).Result;

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