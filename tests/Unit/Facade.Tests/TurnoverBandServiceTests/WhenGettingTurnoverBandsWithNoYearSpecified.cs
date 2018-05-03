namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTurnoverBandsWithNoYearSpecified : ContextBase
    {
        private IEnumerable<ProposedTurnoverBand> proposedTurnoverBands;

        [SetUp]
        public void SetUp()
        {
            this.proposedTurnoverBands = new List<ProposedTurnoverBand> { new ProposedTurnoverBand { Id = 808 } };
            this.ProposedTurnoverBandService.DefaultFinancialYear().Returns("2018/19");
            this.ProposedTurnoverBandRepository.GetAllForFinancialYear("2018/19")
                .Returns(this.proposedTurnoverBands);
            this.Results = this.Sut.GetProposedTurnoverBands(null);
        }

        [Test]
        public void ShouldGetFromRepositoryUsingDefaultYear()
        {
            this.ProposedTurnoverBandRepository.Received().GetAllForFinancialYear("2018/19");
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Results.Should().BeOfType<SuccessResult<IEnumerable<ProposedTurnoverBand>>>();
            var results = ((SuccessResult<IEnumerable<ProposedTurnoverBand>>)this.Results).Data.ToList();
            results.Count.Should().Be(1);
            results.First().Id.Should().Be(808);
        }
    }
}
