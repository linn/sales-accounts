namespace Linn.SalesAccounts.Service.Tests.TurnoverBandModuleTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Models;
    using Linn.SalesAccounts.Resources;
    using Linn.SalesAccounts.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTurnoverBands : ContextBase
    {
        private ProposedTurnoverBandRequestResource requestResource;

        private List<ProposedTurnoverBand> proposedTurnoverBands;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ProposedTurnoverBandRequestResource { FinancialYear = "2018/19" };
            this.proposedTurnoverBands = new List<ProposedTurnoverBand>
                                             {
                                                 new ProposedTurnoverBand
                                                     {
                                                         CalculatedTurnoverBandUri = "/1",
                                                         SalesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "one"))
                                                     },
                                                 new ProposedTurnoverBand
                                                     {
                                                         CalculatedTurnoverBandUri = "/2",
                                                         SalesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 2, "two"))
                                                     }
                                             };
            var proposal = new TurnoverBandProposal(this.requestResource.FinancialYear, this.proposedTurnoverBands);
            this.TurnoverBandService.GetProposedTurnoverBands(this.requestResource.FinancialYear)
                .Returns(new SuccessResult<TurnoverBandProposal>(proposal));
            this.Response = this.Browser.Get(
                "/sales/accounts/turnover-band-proposals",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("financialYear", this.requestResource.FinancialYear);
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
            this.Response.ContentType.Should().Be("application/vnd.linn.sales.accounts-turnover-band-proposal+json;version=1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<TurnoverBandProposalResource>().ProposedTurnoverBands.ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.CalculatedTurnoverBandUri == "/1");
            resources.Should().Contain(a => a.CalculatedTurnoverBandUri == "/2");
        }

        [Test]
        public void ShoudlHaveSelfRef()
        {
            var result = this.Response.Body.DeserializeJson<TurnoverBandProposalResource>();
            result.Links.First(a => a.Rel == "self").Href
                .Should().Be("/sales/accounts/turnover-band-proposals?financialYear=2018/19");
        }

        [Test]
        public void ShoudlHaveApplyRef()
        {
            var result = this.Response.Body.DeserializeJson<TurnoverBandProposalResource>();
            result.Links.First(a => a.Rel == "apply-proposal").Href
                .Should().Be("/sales/accounts/turnover-band-proposals/apply?financialYear=2018/19");
        }
    }
}