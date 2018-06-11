namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenCreatingSalesAccount : ContextBase
    {
        private SalesAccountCreateActivity createActivity;

        private SalesAccount result;

        [SetUp]
        public void SetUp()
        {
            this.createActivity = new SalesAccountCreateActivity("/employees/100", 1, "New Account", 1.December(2018));
            this.result = new SalesAccount(this.createActivity);
        }

        [Test]
        public void ShouldCreateAccount()
        {
            this.result.Id.Should().Be(this.createActivity.AccountId);
            this.result.Name.Should().Be(this.createActivity.Name);
            this.result.ClosedOn.Should().Be(this.createActivity.ClosedOn);
        }

        [Test]
        public void ShouldAddActivity()
        {
            this.result.Activities.Should().HaveCount(1);
            var activity = this.result.Activities.First();
            activity.Should().BeOfType<SalesAccountCreateActivity>();
            ((SalesAccountCreateActivity)activity).UpdatedByUri.Should().Be("/employees/100");
            ((SalesAccountCreateActivity)activity).ClosedOn.Should().Be(1.December(2018));
            ((SalesAccountCreateActivity)activity).AccountId.Should().Be(1);
            ((SalesAccountCreateActivity)activity).Name.Should().Be("New Account");
        }
    }
}
