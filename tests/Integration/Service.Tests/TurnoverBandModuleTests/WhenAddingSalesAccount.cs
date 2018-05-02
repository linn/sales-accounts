namespace Linn.SalesAccounts.Service.Tests.TurnoverBandModuleTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources;
    using Linn.SalesAccounts.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenProposingTurnoverBands : ContextBase
    {
        private List<ProposedTurnoverBand> proposedTurnoverBands;

        private ProposedTurnoverBandRequestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ProposedTurnoverBandRequestResource { FinancialYear = "2018/19" };
            this.proposedTurnoverBands = new List<ProposedTurnoverBand>
                                             {
                                                 new ProposedTurnoverBand { SalesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "one")) },
                                                 new ProposedTurnoverBand { SalesAccount = new SalesAccount(new SalesAccountCreateActivity(2, "two")) }
                                             };
            this.TurnoverBandService.ProposeTurnoverBands("2018/19")
                .Returns(new SuccessResult<IEnumerable<ProposedTurnoverBand>>(this.proposedTurnoverBands));
            this.Response = this.Browser.Post(
                "/sales/accounts/proposed-turnover-bands",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
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
            this.Response.ContentType.Should().Be("application/vnd.linn.sales.account-proposed-turnover-bands+json;version=1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ProposedTurnoverBandResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.SalesAccountId == 1);
            resources.Should().Contain(a => a.SalesAccountId == 2);
        }
    }
}