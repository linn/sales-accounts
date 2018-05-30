namespace Linn.SalesAccounts.Domain.Services
{
    using Linn.SalesAccounts.Domain.External;

    public interface IDiscountingService
    {
        DiscountScheme GetDiscountScheme(string discountSchemeUri);

        string GetTurnoverBandForTurnoverValue(string turnoverBandSetUri, string currencyCode, decimal turnoverValue);

        TurnoverBand GetTurnoverBand(string turnoverBandUri);
    }
}
