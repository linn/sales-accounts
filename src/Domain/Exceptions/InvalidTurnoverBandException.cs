namespace Linn.SalesAccounts.Domain.Exceptions
{
    using System;

    public class InvalidTurnoverBandException : DomainException
    {
        public InvalidTurnoverBandException(string message)
            : base(message)
        {
        }

        public InvalidTurnoverBandException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
