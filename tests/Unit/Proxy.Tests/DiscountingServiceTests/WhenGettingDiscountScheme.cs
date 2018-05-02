namespace Linn.SalesAccounts.Proxy.Tests.DiscountingServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;

    using FluentAssertions;

    using Linn.Common.Proxy;
    using Linn.Common.Resources;
    using Linn.Common.Serialization.Json;
    using Linn.SalesAccounts.Resources.External;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDiscountScheme : ContextBase
    {
        private RestResponse<string> discountSchemeResponse;

        private RestResponse<string> turnoverBandSetResponse;

        [SetUp]
        public void SetUp()
        {
            var json = new JsonSerializer();
            var discountSchemeResource = new DiscountSchemeResource
                                             {
                                                 Name = "discount scheme",
                                                 Links = new[]
                                                             {
                                                                 new LinkResource("self", "/ds/1"),
                                                                 new LinkResource("turnover-band-set", "/tbs/1")
                                                             }
                                             };
            var turnoverBandSetResource = new TurnoverBandSetResource
                                      {
                                          Name = "turnover band set",
                                          TurnoverBands =
                                              new[]
                                                  {
                                                      new TurnoverBandResource
                                                          {
                                                              Name = "tb1",
                                                              Links = new[] { new LinkResource("self", "/tb/1") }
                                                          },
                                                      new TurnoverBandResource
                                                          {
                                                              Name = "/tb/2",
                                                              Links = new[] { new LinkResource("self", "/tb/2") }
                                                          }
                                                  },
                                          Links = new[] { new LinkResource("self", "/tbs/1") }
                                      };
            this.discountSchemeResponse =
                new RestResponse<string> { StatusCode = HttpStatusCode.OK, Value = json.Serialize(discountSchemeResource) };
            this.turnoverBandSetResponse =
                new RestResponse<string> { StatusCode = HttpStatusCode.OK, Value = json.Serialize(turnoverBandSetResource) };

            this.RestClient.Get(
                    Arg.Any<CancellationToken>(),
                    Arg.Is<Uri>(u => u.ToString().Contains("/ds/1")),
                    Arg.Any<IDictionary<string, string>>(),
                    Arg.Any<IDictionary<string, string[]>>())
                .Returns(this.discountSchemeResponse);
            this.RestClient.Get(
                    Arg.Any<CancellationToken>(),
                    Arg.Is<Uri>(u => u.ToString().Contains("/tbs/1")),
                    Arg.Any<IDictionary<string, string>>(),
                    Arg.Any<IDictionary<string, string[]>>())
                .Returns(this.turnoverBandSetResponse);

            this.Result = this.Sut.GetDiscountScheme("/ds/1");
        }

        [Test]
        public void ShouldReturnDiscountScheme()
        {
            this.Result.DiscountSchemeUri.Should().Be("/ds/1");
            this.Result.Name.Should().Be("discount scheme");
            this.Result.TurnoverBandSetUri.Should().Be("/tbs/1");
            this.Result.TurnoverBandUris.Should().HaveCount(2);
            this.Result.TurnoverBandUris.Should().Contain(a => a == "/tb/1");
            this.Result.TurnoverBandUris.Should().Contain(a => a == "/tb/2");
        }
    }
}