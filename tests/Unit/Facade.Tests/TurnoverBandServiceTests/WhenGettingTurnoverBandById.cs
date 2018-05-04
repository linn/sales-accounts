namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTurnoverBandById : ContextBase
    {
        private ProposedTurnoverBand proposedTurnoverBand;

        [SetUp]
        public void SetUp()
        {
            this.proposedTurnoverBand = new ProposedTurnoverBand { Id = 808 };
            this.ProposedTurnoverBandRepository.GetById(808).Returns(this.proposedTurnoverBand);
            this.Result = this.Sut.GetProposedTurnoverBand(808);
        }

        [Test]
        public void ShouldGetFromRepository()
        {
            this.ProposedTurnoverBandRepository.Received().GetById(808);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.Result.Should().BeOfType<SuccessResult<ProposedTurnoverBand>>();
            var result = ((SuccessResult<ProposedTurnoverBand>)this.Result).Data;
            result.Id.Should().Be(808);
        }
    }
}
