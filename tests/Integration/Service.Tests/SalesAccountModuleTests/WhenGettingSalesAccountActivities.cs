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
            var address = new SalesAccountAddress("ln1", "ln2", "", "", "/countries/UK", "post");

            this.salesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "name")) { Id = 1 };
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
            resource.Activities.Any(a => a.ActivityType == "SalesAccountGrowthPartnerActivity").Should().Be(true);
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateDiscountSchemeUriActivity").Should().Be(true);
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateTurnoverBandUriActivity").Should().Be(true);
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateGoodCreditActivity").Should().Be(true);
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateRebateActivity").Should().Be(true);
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateAddressActivity").Should().Be(true);
            resource.Activities.Any(a => a.ActivityType == "SalesAccountUpdateNameActivity").Should().Be(true);
            resource.Activities.Any(a => a.ActivityType == "SalesAccountCloseActivity").Should().Be(true);
        }
    }
}