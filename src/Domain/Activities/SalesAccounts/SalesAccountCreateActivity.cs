namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    using System;

    public class SalesAccountCreateActivity : SalesAccountActivity
    {
        public SalesAccountCreateActivity(int accountId, string name, DateTime? closedOn = null)
        {
            this.AccountId = accountId;
            this.Name = name;
            this.ClosedOn = closedOn;
        }

        private SalesAccountCreateActivity()
        {
            // ef
        }

        public int AccountId { get; private set; }

        public string Name { get; private set; }

        public DateTime? ClosedOn { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}