namespace Linn.SalesAccounts.Service.Modules
{
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SalesAccountModule : NancyModule
    {
        private readonly ISalesAccountService salesAccountService;

        public SalesAccountModule(ISalesAccountService salesAccountService)
        {
            this.salesAccountService = salesAccountService;

            this.Get("/sales/accounts/{id:int}", parameters => this.GetSalesAccount(parameters.id));
            this.Post("/sales/accounts", _ => this.AddSalesAccount());
            this.Put("/sales/accounts/{id:int}", parameters => this.UpdateSalesAccount(parameters.id));
            this.Delete("/sales/accounts/{id:int}", parameters => this.CloseSalesAccount(parameters.id));
        }

        private object GetSalesAccount(int id)
        {
            var salesAccount = this.salesAccountService.GetSalesAccount(id);
            return this.Negotiate.WithModel(salesAccount);
        }

        private object AddSalesAccount()
        {
            var resource = this.Bind<SalesAccountCreateResource>();
            var salesAccount = this.salesAccountService.AddSalesAccount(resource);
            return this.Negotiate.WithModel(salesAccount);
        }

        private object UpdateSalesAccount(int id)
        {
            var resource = this.Bind<SalesAccountUpdateResource>();
            var salesAccount = this.salesAccountService.UpdateSalesAccount(id, resource);
            return this.Negotiate.WithModel(salesAccount);
        }

        private object CloseSalesAccount(int id)
        {
            var resource = this.Bind<SalesAccountCloseResource>();
            var salesAccount = this.salesAccountService.CloseSalesAccount(id, resource);
            return this.Negotiate.WithModel(salesAccount);
        }
    }
}
