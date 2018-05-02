namespace Linn.SalesAccounts.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Services;

    public class TurnoverBandService : ITurnoverBandService
    {
        private readonly ITransactionManager transactionManager;

        private readonly IProposedTurnoverBandService proposedTurnoverBandService;

        public TurnoverBandService(
            ITransactionManager transactionManager,
            IProposedTurnoverBandService proposedTurnoverBandService)
        {
            this.transactionManager = transactionManager;
            this.proposedTurnoverBandService = proposedTurnoverBandService;
        }

        public IResult<IEnumerable<ProposedTurnoverBand>> ProposeTurnoverBands(string financialYear)
        {
            var results = this.proposedTurnoverBandService.CalculateProposedTurnoverBands(financialYear);
            this.transactionManager.Commit();

            return new SuccessResult<IEnumerable<ProposedTurnoverBand>>(results);
        }
    }
}