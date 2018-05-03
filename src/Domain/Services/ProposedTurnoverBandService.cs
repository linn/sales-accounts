namespace Linn.SalesAccounts.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.SalesAccounts.Domain.External;
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
            var proposedTurnoverBands = this.proposedTurnoverBandRepository.GetAllForFinancialYear(financialYear).ToList();
            var sales = this.salesReportingService.GetSalesByAccount(financialYear).ToList();
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
                        var salesData =
                            sales.FirstOrDefault(s => s.Id == salesAccount.Id.ToString())
                            ?? new SalesDataDetail { CurrencyValue = 0, BaseValue = 0, Id = salesAccount.Id.ToString(), Name = salesAccount.Name };
                        var turnoverBandUri = this.discountingService.GetTurnoverBandForTurnoverValue(
                            discountScheme.TurnoverBandSetUri,
                            salesData.CurrencyCode,
                            salesData.CurrencyValue);
                        if (!string.IsNullOrEmpty(turnoverBandUri) || !string.IsNullOrEmpty(salesAccount.TurnoverBandUri))
                        {
                            var proposedTurnoverBand = proposedTurnoverBands.FirstOrDefault(t => t.SalesAccount.Id == salesAccount.Id);
                            if (proposedTurnoverBand == null)
                            {
                                proposedTurnoverBand = new ProposedTurnoverBand
                                                           {
                                                               SalesAccount = salesAccount,
                                                               FinancialYear = financialYear,
                                                               CalculatedTurnoverBandUri = turnoverBandUri,
                                                               ProposedTurnoverBandUri = turnoverBandUri,
                                                               IncludeInUpdate = true,
                                                               SalesValueBase = salesData.BaseValue,
                                                               SalesValueCurrency = salesData.CurrencyValue
                                                           };
                                proposedTurnoverBands.Add(proposedTurnoverBand);
                                this.proposedTurnoverBandRepository.Add(proposedTurnoverBand);
                            }
                            else
                            {
                                proposedTurnoverBand.CalculatedTurnoverBandUri = turnoverBandUri;
                                proposedTurnoverBand.ProposedTurnoverBandUri = turnoverBandUri;
                                proposedTurnoverBand.SalesValueBase = salesData.BaseValue;
                                proposedTurnoverBand.SalesValueCurrency = salesData.CurrencyValue;
                                proposedTurnoverBand.IncludeInUpdate = true;
                            }
                        }
                    }
                }
            }

            return proposedTurnoverBands;
        }
    }
}