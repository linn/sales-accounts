﻿namespace Linn.SalesAccounts.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
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

        public IResult<IEnumerable<ProposedTurnoverBand>> ProposeTurnoverBands(string financialYear)
        {
            var results = this.proposedTurnoverBandService.CalculateProposedTurnoverBands(financialYear);
            this.transactionManager.Commit();

            return new SuccessResult<IEnumerable<ProposedTurnoverBand>>(results);
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

        public IResult<IEnumerable<ProposedTurnoverBand>> GetProposedTurnoverBands(string financialYear)
        {
            return new SuccessResult<IEnumerable<ProposedTurnoverBand>>(
                this.proposedTurnoverBandRepository.GetAllForFinancialYear(financialYear));
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