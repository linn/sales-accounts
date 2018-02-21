namespace Linn.SalesAccounts.Domain
{
    using Linn.SalesAccounts.Domain.Exceptions;
    using System;

    public class SalesAccountAddress
    {
        public SalesAccountAddress(string line1, string line2, string line3, string line4, string countryUri, string postcode)
        {
            if (string.IsNullOrWhiteSpace(line1) &&
                string.IsNullOrWhiteSpace(line2) &&
                string.IsNullOrWhiteSpace(line3) &&
                string.IsNullOrWhiteSpace(line4))
            {
                throw new DomainException("Must specify at least one address line");
            }

            if (countryUri == null)
            {
               throw new DomainException("Must specify a country");
            }

            this.Line1 = line1;
            this.Line2 = line2;
            this.Line3 = line3;
            this.Line4 = line4;
            this.CountryUri = countryUri;
            this.Postcode = postcode;
        }

       
        private SalesAccountAddress()
        {
            //ef
        }

        public string Line1 { get; private set; }

        public string Line2 { get; private set; }

        public string Line3 { get; private set; }

        public string Line4 { get; private set; }

        public string CountryUri { get; private set; }

        public string Postcode { get; private set; }
    }
}
