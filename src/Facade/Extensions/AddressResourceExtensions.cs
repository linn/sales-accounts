using Linn.SalesAccounts.Domain;
using Linn.SalesAccounts.Resources.Messaging;

namespace Linn.SalesAccounts.Facade.Extensions
{
    public static class AddressResourceExtensions
    {
        public static SalesAccountAddress ToDomain(this AddressResource address)
        {
            return new SalesAccountAddress(address.Line1, address.Line2, address.Line3, address.Line4, address.CountryUri, address.Postcode);
        }
    }
}
