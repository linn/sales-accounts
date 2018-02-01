namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public class WhenUpdatingName : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.UpdateName("New Name");
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            this.Sut.Name.Should().Be("New Name");
        }

        [Test]
        public void ShouldAddActivities()
        {
            this.ActivitiesExcludingCreate().Should().HaveCount(1);
            this.ActivitiesExcludingCreate()
                .First(a => a is SalesAccountUpdateNameActivity)
                .As<SalesAccountUpdateNameActivity>().Name.Should().Be("New Name");
        }
    }
}
