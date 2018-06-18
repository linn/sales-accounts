namespace Linn.SalesAccounts.Service.Modules
{
    using Linn.SalesAccounts.Service.Models;

    using Nancy;
    using Nancy.Responses;

    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get("/", args => new RedirectResponse("/sales/accounts"));
            this.Get("/sales", args => new RedirectResponse("/sales/accounts"));
            this.Get("/sales/accounts/signin-oidc-client", _ => this.GetApp());

            this.Get("/sales/accounts/signin-oidc-silent", _ => this.SilentRenew());
        }

        private object GetApp()
        {
            return this.Negotiate.WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index");
        }
        
        private object SilentRenew()
        {
            return this.Negotiate.WithView("silent-renew");
        }
    }
}