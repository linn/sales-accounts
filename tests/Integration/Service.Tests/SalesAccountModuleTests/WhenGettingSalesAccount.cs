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

    public class WhenGettingSalesAccount : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity(1, 1, "name")) { Id = 111 };
            this.SalesAccountRepository.GetById(111).Returns(this.salesAccount);

            this.Response = this.Browser.Get(
                "/sales/accounts/111",
                with => { with.Header("Accept", "application/json"); }).Result;
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
            resource.Name.Should().Be(this.salesAccount.Name);
            resource.Links.Length.Should().Be(1);
            resource.Links.Should().Contain(l => l.Rel == "self" && l.Href == "/sales/accounts/111");
        }
    }
}