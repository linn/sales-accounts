namespace Linn.SalesAccounts.Domain
{
    using System.Collections.Generic;

    using Linn.SalesAccounts.Domain.Activities;

    public abstract class ActivityEntity<T> where T : Activity
    {
        public int Id { get; set; }

        public IList<T> Activities { get; private set; } = new List<T>();
    }
}
