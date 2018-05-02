namespace Linn.SalesAccounts.Domain.Services
{
    using System.Collections.Generic;

    using Linn.SalesAccounts.Domain.Repositories;

    public class ProposedTurnoverBandService : IProposedTurnoverBandService
    {
        private readonly IProposedTurnoverBandRepository proposedTurnoverBandRepository;

        private readonly ISalesAccountRepository salesAccountRepository;

        private readonly ISalesReportingService salesReportingService;

        private readonly IDiscountingService discountingService;

        public ProposedTurnoverBandService(
            IProposedTurnoverBandRepository proposedTurnoverBandRepository,
            ISalesAccountRepository salesAccountRepository,
            ISalesReportingService salesReportingService,
            IDiscountingService discountingService)
        {
            this.proposedTurnoverBandRepository = proposedTurnoverBandRepository;
            this.salesAccountRepository = salesAccountRepository;
            this.salesReportingService = salesReportingService;
            this.discountingService = discountingService;
        }

        public IEnumerable<ProposedTurnoverBand> CalculateProposedTurnoverBands(string financialYear)
        {
            var proposedTurnoverBands = this.proposedTurnoverBandRepository.GetAllForFinancialYear(financialYear);
            var sales = this.salesReportingService.GetSalesByAccount(financialYear);
            var salesAccounts = this.salesAccountRepository.GetAllOpenAccounts();

            foreach (var proposedTurnoverBand in proposedTurnoverBands)
            {
                proposedTurnoverBand.IncludeInUpdate = false;
            }

            foreach (var salesAccount in salesAccounts)
            {
                if (!string.IsNullOrEmpty(salesAccount.DiscountSchemeUri))
                {
                    var discountScheme = this.discountingService.GetDiscountScheme(salesAccount.DiscountSchemeUri);
                    if (!string.IsNullOrEmpty(discountScheme.TurnoverBandSetUri))
                    {
                        // get the proposed band.
                    }
                }
            }

            return null;
        }
    }
}