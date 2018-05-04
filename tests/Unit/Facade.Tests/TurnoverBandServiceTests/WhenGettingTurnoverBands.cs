namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTurnoverBands : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> proposedTurnoverBands;

        private string financialYear;

        [SetUp]
        public void SetUp()
        {
            this.financialYear = "2018/19";
            this.proposedTurnoverBands = new List<ProposedTurnoverBand> { new ProposedTurnoverBand { Id = 808 } };
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear(this.financialYear)
                .Returns(this.proposedTurnoverBands);
            this.Results = this.Sut.GetProposedTurnoverBands(this.financialYear);
        }

        [Test]
        public void ShouldGetFromRepository()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear(this.financialYear);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Results.Should().BeOfType<SuccessResult<TurnoverBandProposal>>();
            var result = ((SuccessResult<TurnoverBandProposal>)this.Results).Data;
            result.FinancialYear.Should().Be(this.financialYear);
            var results = result.ProposedTurnoverBands.ToList();
            results.Count.Should().Be(1);
            results.First().Id.Should().Be(808);
        }
    }
}
