namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    using Linn.SalesAccounts.Domain.Activities;

    public abstract class SalesAccountActivity : Activity
    {
        protected SalesAccountActivity(string updatedByUri) : base(updatedByUri)
        {
        }

        protected SalesAccountActivity()
        {
        }

        public abstract T Accept<T>(ISalesAccountActivityVisitor<T> visitor);
    }
}