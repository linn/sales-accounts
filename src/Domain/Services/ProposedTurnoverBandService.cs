namespace Linn.SalesAccounts.Domain.Services
{
    using System.Collections.Generic;

    using Linn.SalesAccounts.Domain.Repositories;

    public class ProposedTurnoverBandService : IProposedTurnoverBandService
    {
        private readonly IProposedTurnoverBandRepository proposedTurnoverBandRepository;

        private readonly ISalesReportingService salesReportingService;

        public ProposedTurnoverBandService(
            IProposedTurnoverBandRepository proposedTurnoverBandRepository,
            ISalesReportingService salesReportingService)
        {
            this.proposedTurnoverBandRepository = proposedTurnoverBandRepository;
            this.salesReportingService = salesReportingService;
        }

        public IEnumerable<ProposedTurnoverBand> CalculateProposedTurnoverBands(string financialYear)
        {
            throw new System.NotImplementedException();
        }
    }
}