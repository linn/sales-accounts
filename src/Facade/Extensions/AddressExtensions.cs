namespace Linn.SalesAccounts.Facade.Extensions
{
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.Messaging;

    public static class AddressExtensions
    {
        public static AddressResource ToResource(this SalesAccountAddress address)
        {
            return new AddressResource
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                Line3 = address.Line3,
                Line4 = address.Line4,
                CountryUri = address.CountryUri,
                Postcode = address.Postcode
            };
        }
    }
}
