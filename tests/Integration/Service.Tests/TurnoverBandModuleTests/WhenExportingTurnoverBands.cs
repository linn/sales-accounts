namespace Linn.SalesAccounts.Service.Tests.TurnoverBandModuleTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Domain.Models;
    using Linn.SalesAccounts.Facade.Extensions;
    using Linn.SalesAccounts.Resources.RequestResources;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenExportingTurnoverBands : ContextBase
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
                                                         SalesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "one"))
                                                     },
                                                 new ProposedTurnoverBand
                                                     {
                                                         CalculatedTurnoverBandUri = "/2",
                                                         SalesAccount = new SalesAccount(new SalesAccountCreateActivity(2, "two"))
                                                     }
                                             };
            var turnoverBand = new TurnoverBand { Name = "n", TurnoverBandUri = "/1" };
            var proposal = new TurnoverBandProposal(this.requestResource.FinancialYear, this.proposedTurnoverBands);
            this.TurnoverBandService.GetProposedTurnoverBandModelResults(this.requestResource.FinancialYear)
                .Returns(new SuccessResult<IEnumerable<ProposedTurnoverBandModel>>(proposal.ProposedTurnoverBands.Select(a => a.ToModel(turnoverBand, turnoverBand, turnoverBand))));
            this.Response = this.Browser.Get(
                "/sales/accounts/turnover-band-proposals/export",
                with =>
                {
                    with.Query("financialYear", this.requestResource.FinancialYear);
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}