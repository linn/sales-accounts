namespace Linn.SalesAccounts.Domain.Dispatchers.Extensions
{
    using Linn.SalesAccounts.Domain.Dispatchers.Messages;

    public static class SalesAccountMessageExtensions
    {
        public static SalesAccountMessage ToMessage(this SalesAccount salesAccount)
        {
            return new SalesAccountMessage
                       {
                           Id = salesAccount.Id,
                           Href = $"/sales/accounts/{salesAccount.Id}",
                           Name = salesAccount.Name,
                           DiscountSchemeUri = salesAccount.DiscountSchemeUri,
                           EligibleForGoodCreditDiscount = salesAccount.EligibleForGoodCreditDiscount,
                           EligibleForRebate = salesAccount.EligibleForRebate,
                           GrowthPartner = salesAccount.GrowthPartner,
                           TurnoverBandUri = salesAccount.TurnoverBandUri,
                           ClosedOn = salesAccount.ClosedOn?.ToString("o"),
                           OnBoardingAccount = salesAccount.OnBoardingAccount
                       };
        }
    }
}