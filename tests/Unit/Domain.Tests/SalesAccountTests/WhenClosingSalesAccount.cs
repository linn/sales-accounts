namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenClosingSalesAccount : ContextBase
    {
        private SalesAccountCloseActivity closeActivity;

        [SetUp]
        public void SetUp()
        {
            this.closeActivity = new SalesAccountCloseActivity("/employees/100", 1.December(2018));
            this.Sut.CloseAccount(this.closeActivity);
        }

        [Test]
        public void ShouldCloseAccount()
        {
            this.Sut.ClosedOn.Should().Be(this.closeActivity.ClosedOn);
        }

        [Test]
        public void ShouldAddActivity()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(1);
            var activity = this.ActivitiesExcludingCreate().First();
            activity.Should().BeOfType<SalesAccountCloseActivity>();
            ((SalesAccountCloseActivity)activity).ClosedOn.Should().Be(this.closeActivity.ClosedOn);
            ((SalesAccountCloseActivity)activity).UpdatedByUri.Should().Be("/employees/100");
        }
    }
}
