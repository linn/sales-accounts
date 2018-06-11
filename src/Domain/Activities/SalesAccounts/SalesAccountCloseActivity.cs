namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    using System;

    public class SalesAccountCloseActivity : SalesAccountActivity
    {
        public SalesAccountCloseActivity(string updatedByUri, DateTime closedOn) : base(updatedByUri)
        {
            this.ClosedOn = closedOn;
        }

        private SalesAccountCloseActivity()
        {
            // ef
        }

        public DateTime ClosedOn { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}