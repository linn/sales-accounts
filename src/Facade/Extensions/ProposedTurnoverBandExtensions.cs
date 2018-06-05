namespace Linn.SalesAccounts.Facade.Extensions
{
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.External;
    using Linn.SalesAccounts.Domain.Models;

    public static class ProposedTurnoverBandExtensions
    {
        public static ProposedTurnoverBandModel ToModel(
            this ProposedTurnoverBand domain,
            TurnoverBand currentTurnoverBand,
            TurnoverBand calculatedTurnoverBand,
            TurnoverBand proposedTurnoverBand)
        {
            return new ProposedTurnoverBandModel
            {
                AppliedToAccount = domain.AppliedToAccount,
                IncludeInUpdate = domain.IncludeInUpdate,
                FinancialYear = domain.FinancialYear,
                SalesValueCurrency = domain.SalesValueCurrency,
                CurrencyCode = domain.CurrencyCode,
                SalesValueBase = domain.SalesValueBase,
                SalesAccountId = domain.SalesAccount.Id,
                SalesAccountName = domain.SalesAccount.Name,
                CurrentTurnoverBandName = currentTurnoverBand?.Name,
                CalculatedTurnoverBandName = calculatedTurnoverBand?.Name,
                ProposedTurnoverBandName = proposedTurnoverBand?.Name
            };
        }
    }
}
