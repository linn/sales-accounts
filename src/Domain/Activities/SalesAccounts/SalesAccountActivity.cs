namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    using Linn.SalesAccounts.Domain.Activities;

    public abstract class SalesAccountActivity : Activity
    {
        public abstract T Accept<T>(ISalesAccountActivityVisitor<T> visitor);
    }
}