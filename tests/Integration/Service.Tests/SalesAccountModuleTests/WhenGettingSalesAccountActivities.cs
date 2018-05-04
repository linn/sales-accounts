namespace Linn.SalesAccounts.Service.Tests.SalesAccountModuleTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSalesAccountActivities : ContextBase
    {
        private SalesAccount salesAccount;

        [SetUp]
        public void SetUp()
        {
            var discountScheme = new DiscountScheme { DiscountSchemeUri = "/ds/1", TurnoverBandUris = new[] { "/tb/1" } };
            var address = new SalesAccountAddress("ln1", "ln2", string.Empty, string.Empty, "/countries/UK", "post");

            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "name", 4.May(2018))) { Id = 1 };
            this.salesAccount.UpdateAccount(discountScheme, "/tb/1", true, true, true);
            this.salesAccount.UpdateNameAndAddress("new name", address);
            this.salesAccount.CloseAccount(new SalesAccountCloseActivity(17.April(2018)));

            this.SalesAccountRepository.GetById(1).Returns(this.salesAccount);

            this.Response = this.Browser.Get(
                "/sales/accounts/1/activities",
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
            this.Response.ContentType.Should().Be("application/vnd.linn.sales.account.activities+json;version=1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<SalesAccountActivitiesResource>();
            resource.Activities.Count().Should().Be(9);
            resource.Activities.First().ActivityType.Should().Be("SalesAccountCreateActivity");
            resource.Activities.Any(a => a.ActivityType == "SalesAccountGrowthPartnerActivity").Should().BeTrue();
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateDiscountSchemeUriActivity").Should().BeTrue();
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateTurnoverBandUriActivity").Should().BeTrue();
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateGoodCreditActivity").Should().BeTrue();
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateRebateActivity").Should().BeTrue();
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateAddressActivity").Should().BeTrue();
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateNameActivity").Should().BeTrue();
            resource.Activities.Any(a => a.ActivityType == "SalesAccountCloseActivity").Should().BeTrue();
        }
    }
}