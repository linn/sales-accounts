namespace Linn.SalesAccounts.Proxy.Tests.SalesReportingServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using FluentAssertions;

    using Linn.Common.Proxy;
    using Linn.Common.Serialization.Json;
    using Linn.SalesAccounts.Domain.External;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSalesByAccount : ContextBase
    {
        private RestResponse<string> salesResponse;

        [SetUp]
        public void SetUp()
        {
            var json = new JsonSerializer();
            var items = new List<SalesDataDetail>
                            {
                                new SalesDataDetail { CurrencyValue = 1m },
                                new SalesDataDetail { CurrencyValue = 2m }
                            };
            this.salesResponse = new RestResponse<string> { StatusCode = HttpStatusCode.OK, Value = json.Serialize(items) };

            this.RestClient.Get(
                    Arg.Any<CancellationToken>(),
                    Arg.Is<Uri>(u => u.ToString().Contains("/sales/reporting/query") && u.ToString().Contains("financialYear=2018/19")),
                    Arg.Any<IDictionary<string, string>>(),
                    Arg.Any<IDictionary<string, string[]>>())
                .Returns(this.salesResponse);

            this.Results = this.Sut.GetSalesByAccount("2018/19");
        }

        [Test]
        public void ShouldReturnSalesData()
        {
            this.Results.Count().Should().Be(2);
            this.Results.Any(a => a.CurrencyValue == 1m).Should().BeTrue();
            this.Results.Any(a => a.CurrencyValue == 2m).Should().BeTrue();
        }
    }
}