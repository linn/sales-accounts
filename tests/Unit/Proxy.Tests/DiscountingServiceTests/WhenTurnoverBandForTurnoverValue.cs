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

    public class WhenTurnoverBandForTurnoverValue : ContextBase
    {
        private RestResponse<string> turnoverBandResponse;

        private string turnoverBandUriResult;

        [SetUp]
        public void SetUp()
        {
            var json = new JsonSerializer();
            var turnoverBandResource = new TurnoverBandResource { Name = "tb1", Links = new[] { new LinkResource("self", "/tb/1") } };
            this.turnoverBandResponse =
                new RestResponse<string> { StatusCode = HttpStatusCode.OK, Value = json.Serialize(turnoverBandResource) };

            this.RestClient.Get(
                    Arg.Any<CancellationToken>(),
                    Arg.Is<Uri>(u => u.ToString().Contains("/tbs/1") && u.ToString().Contains("GBP") && u.ToString().Contains("11111.90")),
                    Arg.Any<IDictionary<string, string>>(),
                    Arg.Any<IDictionary<string, string[]>>())
                .Returns(this.turnoverBandResponse);

            this.turnoverBandUriResult = this.Sut.GetTurnoverBandForTurnoverValue("/tbs/1", "GBP", 11111.90m);
        }

        [Test]
        public void ShouldReturnTurnoverBandUri()
        {
            this.turnoverBandUriResult.Should().Be("/tb/1");
        }
    }
}