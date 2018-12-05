namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountReopenActivity : SalesAccountActivity
    {
        public SalesAccountReopenActivity(string updatedByUri) : base(updatedByUri)
        {
        }

        private SalesAccountReopenActivity()
        {
            // ef
        }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}