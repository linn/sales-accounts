namespace Linn.SalesAccounts.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;

    public class TurnoverBandService : ITurnoverBandService
    {
        private readonly ITransactionManager transactionManager;

        private readonly IProposedTurnoverBandService proposedTurnoverBandService;

        private readonly IProposedTurnoverBandRepository proposedTurnoverBandRepository;

        public TurnoverBandService(
            ITransactionManager transactionManager,
            IProposedTurnoverBandService proposedTurnoverBandService,
            IProposedTurnoverBandRepository proposedTurnoverBandRepository)
        {
            this.transactionManager = transactionManager;
            this.proposedTurnoverBandService = proposedTurnoverBandService;
            this.proposedTurnoverBandRepository = proposedTurnoverBandRepository;
        }

        public IResult<TurnoverBandProposal> ProposeTurnoverBands(string financialYear)
        {
            var result = this.proposedTurnoverBandService.CalculateProposedTurnoverBands(financialYear);
            this.transactionManager.Commit();

            return new SuccessResult<TurnoverBandProposal>(result);
        }

        public IResult<ProposedTurnoverBand> GetProposedTurnoverBand(int id)
        {
            var proposedBand = this.proposedTurnoverBandRepository.GetById(id);
            if (proposedBand == null)
            {
                return new NotFoundResult<ProposedTurnoverBand>();
            }

            return new SuccessResult<ProposedTurnoverBand>(proposedBand);
        }

        public IResult<TurnoverBandProposal> GetProposedTurnoverBands(string financialYear)
        {
            if (string.IsNullOrEmpty(financialYear))
            {
                financialYear = this.proposedTurnoverBandService.DefaultFinancialYear();
            }

            var proposedTurnoverBands = this.proposedTurnoverBandRepository.GetAllForFinancialYear(financialYear);

            return new SuccessResult<TurnoverBandProposal>(new TurnoverBandProposal(financialYear, proposedTurnoverBands));
        }

        public IResult<ProposedTurnoverBand> OverrideTurnoverBand(int id, string turnoverBandUri)
        {
            var proposedTurnoverBand = this.proposedTurnoverBandRepository.GetById(id);
            if (proposedTurnoverBand == null)
            {
                return new NotFoundResult<ProposedTurnoverBand>();
            }

            proposedTurnoverBand.OverrideProposedTurnoverBand(turnoverBandUri);
            this.transactionManager.Commit();

            return new SuccessResult<ProposedTurnoverBand>(proposedTurnoverBand);
        }
    }
}