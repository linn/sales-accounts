namespace Linn.SalesAccounts.Domain
{
    using System.Collections.Generic;

    using Linn.SalesAccounts.Domain.Activities;

    public abstract class ActivityEntity<T> : Entity where T : Activity
    {
        public IList<T> Activities { get; private set; } = new List<T>();
    }
}
