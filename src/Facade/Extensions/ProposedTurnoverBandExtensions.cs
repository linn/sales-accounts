namespace Linn.SalesAccounts.Facade.Extensions
{
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Models;

    public static class ProposedTurnoverBandExtensions
    {
        public static ProposedTurnoverBandModel ToModel(
            this ProposedTurnoverBand domain,
            string currentTurnoverBandName,
            string calculatedTurnoverBandName,
            string proposedTurnoverBandName)
        {
            return new ProposedTurnoverBandModel
            {
                AppliedToAccount = domain.AppliedToAccount,
                IncludeInUpdate = domain.IncludeInUpdate,
                FinancialYear = domain.FinancialYear,
                SalesValueCurrency = domain.SalesValueCurrency,
                SalesValueBase = domain.SalesValueBase,
                SalesAccountId = domain.SalesAccount.Id,
                SalesAccountName = domain.SalesAccount.Name,
                CurrentTurnoverBandName = currentTurnoverBandName,
                CalculatedTurnoverBandName = calculatedTurnoverBandName,
                ProposedTurnoverBandName = proposedTurnoverBandName
            };
        }
    }
}
