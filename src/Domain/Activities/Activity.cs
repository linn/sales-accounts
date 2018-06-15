namespace Linn.SalesAccounts.Domain.Activities
{
    using System;

    public abstract class Activity : Entity
    {
        protected Activity(string updatedByUri)
        {
            this.UpdatedByUri = updatedByUri;
            this.ChangedOn = DateTime.UtcNow;
        }

        protected Activity()
        {
            // ef
        }

        public string UpdatedByUri { get; set; }

        public DateTime ChangedOn { get; set; }
    }
}