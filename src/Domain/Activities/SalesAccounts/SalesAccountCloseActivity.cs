namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    using System;

    public class SalesAccountCloseActivity : SalesAccountActivity
    {
        public SalesAccountCloseActivity(DateTime closedOn)
        {
            this.ClosedOn = closedOn;
        }

        public DateTime ClosedOn { get; set; }
    }
}