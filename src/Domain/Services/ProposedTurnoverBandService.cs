namespace Linn.SalesAccounts.Domain.Services
{
    using System;
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
            if (string.IsNullOrEmpty(financialYear))
            {
                financialYear = this.DefaultFinancialYear();
            }

            var salesAccounts = this.salesAccountRepository.GetAllOpenAccounts();
            var proposedTurnoverBands = this.proposedTurnoverBandRepository.GetAllForFinancialYear(financialYear).ToList();
            foreach (var proposedTurnoverBand in proposedTurnoverBands)
            {
                proposedTurnoverBand.IncludeInUpdate = false;
            }

            var sales = this.salesReportingService.GetSalesByAccount(financialYear).ToList();

            foreach (var salesAccount in salesAccounts)
            {
                var salesForAccount = this.GetSalesForSalesAccount(sales, salesAccount);
                var proposedTurnoverBandUri = this.GetTurnoverBandForSalesAccount(salesAccount, salesForAccount);

                this.AddOrUpdateProposedTurnoverBand(
                    proposedTurnoverBands,
                    salesAccount,
                    proposedTurnoverBandUri,
                    salesForAccount,
                    financialYear);
            }

            return proposedTurnoverBands;
        }

        public string DefaultFinancialYear()
        {
            var date = new DateTime(DateTime.Now.Year, 1, 1);
            return $"{date.AddYears(-1).Year}/{date:yy}";
        }

        private SalesDataDetail GetSalesForSalesAccount(
            IEnumerable<SalesDataDetail> salesDataDetails,
            SalesAccount salesAccount)
        {
            var sales = salesDataDetails.FirstOrDefault(s => s.Id == salesAccount.Id.ToString());
            return sales ?? new SalesDataDetail
                                {
                                    CurrencyValue = 0m,
                                    BaseValue = 0m,
                                    Id = salesAccount.Id.ToString(),
                                    Name = salesAccount.Name
                                };
        }

        private string GetTurnoverBandForSalesAccount(SalesAccount salesAccount, SalesDataDetail salesData)
        {
            if (string.IsNullOrEmpty(salesAccount.DiscountSchemeUri))
            {
                return null;
            }

            var discountScheme = this.discountingService.GetDiscountScheme(salesAccount.DiscountSchemeUri);
            if (!string.IsNullOrEmpty(discountScheme.TurnoverBandSetUri))
            {
                return this.discountingService.GetTurnoverBandForTurnoverValue(
                    discountScheme.TurnoverBandSetUri,
                    salesData.CurrencyCode,
                    salesData.CurrencyValue);
            }

            return null;
        }

        private void AddOrUpdateProposedTurnoverBand(
            IList<ProposedTurnoverBand> proposedTurnoverBands,
            SalesAccount salesAccount,
            string turnoverBandUri,
            SalesDataDetail salesForAccount,
            string financialYear)
        {
            if (string.IsNullOrEmpty(turnoverBandUri) && string.IsNullOrEmpty(salesAccount.TurnoverBandUri))
            {
                return;
            }

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
                                               SalesValueBase = salesForAccount.BaseValue,
                                               SalesValueCurrency = salesForAccount.CurrencyValue
                                           };
                proposedTurnoverBands.Add(proposedTurnoverBand);
                this.proposedTurnoverBandRepository.Add(proposedTurnoverBand);
            }
            else
            {
                proposedTurnoverBand.CalculatedTurnoverBandUri = turnoverBandUri;
                proposedTurnoverBand.ProposedTurnoverBandUri = turnoverBandUri;
                proposedTurnoverBand.SalesValueBase = salesForAccount.BaseValue;
                proposedTurnoverBand.SalesValueCurrency = salesForAccount.CurrencyValue;
                proposedTurnoverBand.IncludeInUpdate = true;
            }
        }
    }
}