namespace Linn.SalesAccounts.Domain.Activities
{
    using System;

    public abstract class Activity : Entity
    {
        protected Activity()
        {
            this.ChangedOn = DateTime.UtcNow;
        }

        public DateTime ChangedOn { get; set; }
    }
}