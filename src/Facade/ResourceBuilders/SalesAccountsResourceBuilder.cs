namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class SalesAccountsResourceBuilder : IResourceBuilder<IEnumerable<SalesAccount>>
    {
        private readonly SalesAccountResourceBuilder salesAccountResourceBuilder = new SalesAccountResourceBuilder();

        public IEnumerable<SalesAccountResource> Build(IEnumerable<SalesAccount> salesAccounts)
        {
            return salesAccounts.Select(s => this.salesAccountResourceBuilder.Build(s));
        }

        object IResourceBuilder<IEnumerable<SalesAccount>>.Build(IEnumerable<SalesAccount> salesAccounts) => this.Build(salesAccounts);

        public string GetLocation(IEnumerable<SalesAccount> salesAccounts) => "/sales/accounts";
    }
}