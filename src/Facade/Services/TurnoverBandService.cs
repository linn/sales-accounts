namespace Linn.SalesAccounts.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;
    using Linn.SalesAccounts.Domain.Repositories;
    using Linn.SalesAccounts.Domain.Services;
    using Linn.SalesAccounts.Facade.Extensions;

    public class TurnoverBandService : ITurnoverBandService
    {
        private readonly ITransactionManager transactionManager;

        private readonly IProposedTurnoverBandService proposedTurnoverBandService;

        private readonly IProposedTurnoverBandRepository proposedTurnoverBandRepository;

        private readonly IDiscountingService discountingService;

        public TurnoverBandService(
            ITransactionManager transactionManager,
            IProposedTurnoverBandService proposedTurnoverBandService,
            IProposedTurnoverBandRepository proposedTurnoverBandRepository,
            IDiscountingService discountingService)
        {
            this.transactionManager = transactionManager;
            this.proposedTurnoverBandService = proposedTurnoverBandService;
            this.proposedTurnoverBandRepository = proposedTurnoverBandRepository;
            this.discountingService = discountingService;
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

        public IResult<IEnumerable<ProposedTurnoverBandModel>> GetProposedTurnoverBandModelResults(string financialYear)
        {
            if (string.IsNullOrEmpty(financialYear))
            {
                financialYear = this.proposedTurnoverBandService.DefaultFinancialYear();
            }

            var proposedTurnoverBands = this.proposedTurnoverBandRepository.GetAllForFinancialYear(financialYear).ToList();
            var turnoverBandUris = proposedTurnoverBands.Select(a => a.CalculatedTurnoverBandUri)
                .Union(proposedTurnoverBands.Select(a => a.ProposedTurnoverBandUri))
                .Union(proposedTurnoverBands.Select(a => a.SalesAccount.TurnoverBandUri))
                .Distinct();
            var turnoverBands = turnoverBandUris
                .Where(v => !string.IsNullOrEmpty(v))
                .Select(u => this.discountingService.GetTurnoverBand(u)).ToList();

            return new SuccessResult<IEnumerable<ProposedTurnoverBandModel>>(
                proposedTurnoverBands.Select(
                    p => p.ToModel(
                        turnoverBands.FirstOrDefault(a => a.TurnoverBandUri == p.SalesAccount.TurnoverBandUri),
                        turnoverBands.FirstOrDefault(a => a.TurnoverBandUri == p.CalculatedTurnoverBandUri),
                        turnoverBands.FirstOrDefault(a => a.TurnoverBandUri == p.ProposedTurnoverBandUri))));
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

        public IResult<ProposedTurnoverBand> ExcludeFromTurnoverBandProposal(int id)
        {
            var proposedTurnoverBand = this.proposedTurnoverBandRepository.GetById(id);
            if (proposedTurnoverBand == null)
            {
                return new NotFoundResult<ProposedTurnoverBand>();
            }

            proposedTurnoverBand.ExcludeFromUpdate();
            this.transactionManager.Commit();

            return new SuccessResult<ProposedTurnoverBand>(proposedTurnoverBand);
        }

        public IResult<TurnoverBandProposal> ApplyTurnoverBandProposal(string financialYear)
        {
            var proposal = this.proposedTurnoverBandService.ApplyTurnoverBandProposal(financialYear);
            this.transactionManager.Commit();
            return new SuccessResult<TurnoverBandProposal>(proposal);
        }
    }
}