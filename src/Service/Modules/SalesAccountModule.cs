namespace Linn.SalesAccounts.Service.Modules
{
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Resources.SalesAccounts;
    using Linn.SalesAccounts.Service.Extensions;
    using Linn.SalesAccounts.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class SalesAccountModule : NancyModule
    {
        private readonly ISalesAccountService salesAccountService;

        public SalesAccountModule(ISalesAccountService salesAccountService)
        {
            this.salesAccountService = salesAccountService;

            this.Get("/sales/accounts", _ => this.GetSalesAccounts());
            this.Get("/sales/accounts/{id:int}", parameters => this.GetSalesAccount(parameters.id));
            this.Get("/sales/accounts/{id:int}/activities", parameters => this.GetSalesAccountActivities(parameters.id));
            this.Post("/sales/accounts", _ => this.AddSalesAccount());
            this.Put("/sales/accounts/{id:int}", parameters => this.UpdateSalesAccount(parameters.id));
            this.Delete("/sales/accounts/{id:int}", parameters => this.CloseSalesAccount(parameters.id));
        }

        private object GetSalesAccount(int id)
        {
            var salesAccount = this.salesAccountService.GetById(id);
            return this.Negotiate
                .WithModel(salesAccount)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetSalesAccounts()
        {
            var resource = this.Bind<SalesAccountSearchResource>();
            var salesAccounts = this.salesAccountService.Get(resource.SearchTerm);
            return this.Negotiate
                .WithModel(salesAccounts)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetSalesAccountActivities(int id)
        {
            this.RequiresAuthentication();

            var activities = this.salesAccountService.GetActivitiesById(id);
            return this.Negotiate.WithModel(activities);
        }

        private object AddSalesAccount()
        {
            this.RequiresAuthentication();

            var employeeUri = this.Context.CurrentUser.GetEmployeeUri();

            var resource = this.Bind<SalesAccountCreateResource>();
            var salesAccount = this.salesAccountService.AddSalesAccount(resource, employeeUri);
            return this.Negotiate.WithModel(salesAccount);
        }

        private object UpdateSalesAccount(int id)
        {
            this.RequiresAuthentication();

            var employeeUri = this.Context.CurrentUser.GetEmployeeUri();

            var resource = this.Bind<SalesAccountUpdateResource>();
            var salesAccount = this.salesAccountService.UpdateSalesAccount(id, resource, employeeUri);
            return this.Negotiate.WithModel(salesAccount);
        }

        private object CloseSalesAccount(int id)
        {
            this.RequiresAuthentication();

            var employeeUri = this.Context.CurrentUser.GetEmployeeUri();

            var resource = this.Bind<SalesAccountCloseResource>();
            var salesAccount = this.salesAccountService.CloseSalesAccount(id, resource, employeeUri);
            return this.Negotiate.WithModel(salesAccount);
        }
    }
}
