namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SalesAccount Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new SalesAccount(new SalesAccountCreateActivity(10, "Account 10"));
        }

        protected IEnumerable<SalesAccountActivity> ActivitiesExcludingCreate()
        {
            return this.Sut.Activities.Where(a => a.GetType() != typeof(SalesAccountCreateActivity));
        }
    }
}