namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingNotFoundTurnoverBandById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.ProposedTurnoverBandRepository.GetById(808).Returns((ProposedTurnoverBand)null);
            this.Result = this.Sut.GetProposedTurnoverBand(808);
        }

        [Test]
        public void ShouldTryToGetFromRepository()
        {
            this.ProposedTurnoverBandRepository.Received().GetById(808);
        }

        [Test]
        public void ShouldReturnNotFoundResult()
        {
            this.Result.Should().BeOfType<NotFoundResult<ProposedTurnoverBand>>();
        }
    }
}
