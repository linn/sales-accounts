namespace Linn.SalesAccounts.Service.Tests.SalesAccountModuleTests
{
    using FluentAssertions;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenClosingSalesAccount : ContextBase
    {
        private SalesAccount salesAccount;
        private SalesAccountCloseResource salesAccountCloseResource;

        [SetUp]
        public void SetUp()
        {
            this.salesAccountCloseResource = new SalesAccountCloseResource { ClosedOn = "2018-01-30T11:41:53.0000000+00:00" };

            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "name")) { Id = 111 };

            this.SalesAccountRepository.GetById(111).Returns(this.salesAccount);

            this.Response = this.Browser.Delete(
                "/sales/accounts/111",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.salesAccountCloseResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
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
            resource.Name.Should().Be("name");
            resource.ClosedOn.Should().Be("2018-01-30T11:41:53.0000000Z");
        }
    }
}