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
            this.createActivity = new SalesAccountCreateActivity(1, "n", 1.December(2018));
            this.result = new SalesAccount(this.createActivity);
        }

        [Test]
        public void ShouldCreateAccount()
        {
            this.result.AccountId.Should().Be(this.createActivity.AccountId);
            this.result.Name.Should().Be(this.createActivity.Name);
            this.result.ClosedOn.Should().Be(this.createActivity.ClosedOn);
        }

        [Test]
        public void ShouldAddActivity()
        {
            this.result.Activities.Should().HaveCount(1);
            this.result.Activities.First().Should().BeOfType<SalesAccountCreateActivity>();
        }
    }
}
