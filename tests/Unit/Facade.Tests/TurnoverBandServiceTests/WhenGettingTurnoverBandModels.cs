namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Domain.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTurnoverBandModels : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> proposedTurnoverBands;

        private string financialYear;

        private IResult<IEnumerable<ProposedTurnoverBandModel>> results;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            var salesAccount =
                new SalesAccount(new SalesAccountCreateActivity(1, "Name")) { TurnoverBandUri = "/tb/3" };
            this.proposedTurnoverBands = new List<ProposedTurnoverBand>
                                             {
                                                 new ProposedTurnoverBand
                                                     {
                                                         Id = 808,
                                                         SalesAccount = salesAccount,
                                                         SalesValueCurrency = 1.1m,
                                                         ProposedTurnoverBandUri = "/tb/1",
                                                         CalculatedTurnoverBandUri = "/tb/2"
                                                     }
                                             };
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear(this.financialYear)
                .Returns(this.proposedTurnoverBands);
            this.DiscountingService.GetTurnoverBand("/tb/1").Returns(new TurnoverBand { Name = "turnover band 1", TurnoverBandUri = "/tb/1" });
            this.DiscountingService.GetTurnoverBand("/tb/2").Returns(new TurnoverBand { Name = "turnover band 2", TurnoverBandUri = "/tb/2" });
            this.DiscountingService.GetTurnoverBand("/tb/3").Returns(new TurnoverBand { Name = "turnover band 3", TurnoverBandUri = "/tb/3" });
            this.results = this.Sut.GetProposedTurnoverBandModelResults(this.financialYear);
        }

        [Test]
        public void ShouldGetFromRepository()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.financialYear);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ProposedTurnoverBandModel>>>();
            var data = ((SuccessResult<IEnumerable<ProposedTurnoverBandModel>>)this.results).Data.ToList();
            data.Count.Should().Be(1);
            data.First().SalesAccountId.Should().Be(1);
            data.First().SalesValueCurrency.Should().Be(1.1m);
            data.First().ProposedTurnoverBandName.Should().Be("turnover band 1");
            data.First().CalculatedTurnoverBandName.Should().Be("turnover band 2");
            data.First().CurrentTurnoverBandName.Should().Be("turnover band 3");
        }
    }
}
