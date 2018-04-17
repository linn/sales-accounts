namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class SalesAccountActivitiesResourceBuilder : IResourceBuilder<IEnumerable<SalesAccountActivity>>
    {
        private readonly ResourceBuildingSalesAccountActivityVisitor visitor = new ResourceBuildingSalesAccountActivityVisitor();

        public SalesAccountActivitiesResource Build(IEnumerable<SalesAccountActivity> activities)
        {
            return new SalesAccountActivitiesResource { Activities = activities.Select(a => a.Accept(this.visitor)) };
        }

        object IResourceBuilder<IEnumerable<SalesAccountActivity>>.Build(IEnumerable<SalesAccountActivity> activities) =>
            this.Build(activities);

        public string GetLocation(IEnumerable<SalesAccountActivity> model)
        {
            throw new System.NotImplementedException();
        }
    }
}