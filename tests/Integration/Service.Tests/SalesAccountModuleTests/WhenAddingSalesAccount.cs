namespace Linn.SalesAccounts.Service.Tests.SalesAccountModuleTests
{
    using FluentAssertions;

    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Nancy;
    using Nancy.Testing;

    using NUnit.Framework;

    public class WhenAddingSalesAccount : ContextBase
    {
        private SalesAccountCreateResource salesAccountCreateResource;

        [SetUp]
        public void SetUp()
        {
            this.salesAccountCreateResource =
                new SalesAccountCreateResource { Name = "new", AccountId = 2, OutletNumber = 4 };
            this.Response = this.Browser.Post(
                "/sales/accounts",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.salesAccountCreateResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldReturnCorrectContentType()
        {
            this.Response.ContentType.Should().Be("application/vnd.linn.sales.account+json;version=1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<SalesAccountResource>();
            resource.Name.Should().Be(this.salesAccountCreateResource.Name);
        }
    }
}