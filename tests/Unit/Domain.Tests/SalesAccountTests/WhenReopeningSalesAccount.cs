namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenReopeningSalesAccount : ContextBase
    {
        private SalesAccountReopenActivity reopenActivity;

        [SetUp]
        public void SetUp()
        {
            this.Sut.CloseAccount(new SalesAccountCloseActivity("/", DateTime.UtcNow));
            this.reopenActivity = new SalesAccountReopenActivity("/employees/100");
            this.Sut.ReopenAccount(this.reopenActivity);
        }

        [Test]
        public void ShouldReopenAccount()
        {
            this.Sut.ClosedOn.Should().BeNull();
        }

        [Test]
        public void ShouldAddActivity()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(2);
            var activity = this.Sut.Activities.FirstOrDefault(a => a.GetType() == typeof(SalesAccountReopenActivity));
            activity.Should().NotBeNull();
            activity.UpdatedByUri.Should().Be("/employees/100");
        }
    }
}
