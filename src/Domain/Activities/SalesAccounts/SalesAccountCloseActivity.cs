namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    using System;

    public class SalesAccountCloseActivity : SalesAccountActivity
    {
        public SalesAccountCloseActivity(DateTime closedOn)
        {
            this.ClosedOn = closedOn;
        }

        private SalesAccountCloseActivity()
        {
            // ef
        }

        public DateTime ClosedOn { get; private set; }
    }
}