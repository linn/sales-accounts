namespace Linn.SalesAccounts.Domain.Exceptions
{
    using System;

    public class NoFinancialYearSpecifiedException : DomainException
    {
        public NoFinancialYearSpecifiedException(string message)
            : base(message)
        {
        }

        public NoFinancialYearSpecifiedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
