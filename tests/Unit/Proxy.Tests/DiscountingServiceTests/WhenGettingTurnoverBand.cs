namespace Linn.SalesAccounts.Proxy.Tests.DiscountingServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using FluentAssertions;

    using Linn.Common.Proxy;
    using Linn.Common.Resources;
    using Linn.Common.Serialization.Json;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Resources.External;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTurnoverBand : ContextBase
    {
        private RestResponse<string> turnoverBandResponse;

        private TurnoverBand result;

        private TurnoverBandResource turnoverBandResource;

        [SetUp]
        public void SetUp()
        {
            var json = new JsonSerializer();
            this.turnoverBandResource = new TurnoverBandResource
                                      {
                                          Name = "turnover band",
                                          Links = new[] { new LinkResource("self", "/tb/1") }
                                      };
            this.turnoverBandResponse =
                new RestResponse<string> { StatusCode = HttpStatusCode.OK, Value = json.Serialize(this.turnoverBandResource) };

            this.RestClient.Get(
                    Arg.Any<CancellationToken>(),
                    Arg.Is<Uri>(u => u.ToString().Contains("/tb/1")),
                    Arg.Any<IDictionary<string, string>>(),
                    Arg.Any<IDictionary<string, string[]>>())
                .Returns(this.turnoverBandResponse);

            this.result = this.Sut.GetTurnoverBand("/tb/1");
        }

        [Test]
        public void ShouldReturnTurnoverBand()
        {
            this.result.Name.Should().Be(this.turnoverBandResource.Name);
            this.result.TurnoverBandUri.Should().Be(this.turnoverBandResource.Links.First().Href);
        }
    }
}