namespace Linn.SalesAccounts.Domain.Services
{
    using Linn.SalesAccounts.Domain.External;

    public interface IDiscountSchemeService
    {
        DiscountScheme GetDiscountScheme(string discountSchemeUri);
    }
}
