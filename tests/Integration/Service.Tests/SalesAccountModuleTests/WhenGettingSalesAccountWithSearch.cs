namespace Linn.SalesAccounts.Service.Tests.SalesAccountModuleTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSalesAccountWithSearch : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "name")) { Id = 111 };
            this.SalesAccountRepository.Get("search").Returns(new[] { this.salesAccount });

            this.Response = this.Browser.Get(
                "/sales/accounts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "search");
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
            this.Response.ContentType.Should().Be("application/vnd.linn.sales.accounts+json;version=1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<SalesAccountResource>>().ToList();
            resources.Should().HaveCount(1);
            resources.First().AccountId.Should().Be(1);
        }
    }
}