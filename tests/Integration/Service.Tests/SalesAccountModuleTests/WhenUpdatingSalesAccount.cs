namespace Linn.SalesAccounts.Service.Tests.SalesAccountModuleTests
{
    using FluentAssertions;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingSalesAccount : ContextBase
    {
        private SalesAccount salesAccount;
        private SalesAccountUpdateResource salesAccountUpdateResource;
        private DiscountScheme discountScheme;

        [SetUp]
        public void SetUp()
        {
            this.salesAccountUpdateResource = new SalesAccountUpdateResource
                                                  {
                                                      TurnoverBandUri = "/tb/10",
                                                      DiscountSchemeUri = "/ds/10",
                                                      EligibleForGoodCreditDiscount = true
                                                  };

            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "name")) { Id = 111 };

            this.discountScheme = new DiscountScheme { DiscountSchemeUri = "/ds/10", TurnoverBandUris = new[] { "/tb/10" } };
            this.DiscountingService.GetDiscountScheme(this.salesAccountUpdateResource.DiscountSchemeUri)
                .Returns(this.discountScheme);
            this.SalesAccountRepository.GetById(111).Returns(this.salesAccount);

            this.Response = this.Browser.Put(
                "/sales/accounts/111",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.salesAccountUpdateResource);
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
            resource.EligibleForGoodCreditDiscount.Should().BeTrue();
            resource.TurnoverBandUri.Should().Be("/tb/10");
            resource.DiscountSchemeUri.Should().Be("/ds/10");
            resource.Links.Length.Should().Be(1);
            resource.Links.Should().Contain(l => l.Rel == "self" && l.Href == "/sales/accounts/111");
        }
    }
}