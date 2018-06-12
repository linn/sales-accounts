namespace Linn.SalesAccounts.Service.Modules
{
    using Nancy;
    using Nancy.Responses;
    using Nancy.Security;

    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get("/", args => new RedirectResponse("/sales/accounts"));
            this.Get("/sales", args => new RedirectResponse("/sales/accounts"));
            this.Get("/sales/accounts/(.*)", _ => this.GetApp());
            this.Get("/sales/accounts/signin-oidc-client", _ => this.GetApp());

            this.Get("/sales/accounts/signin-oidc-silent", _ => this.SilentRenew());

            this.RequiresAuthentication();
        }

        private object GetApp()
        {
            return this.Negotiate.WithView("Index");
        }
        
        private object SilentRenew()
        {
            return this.Negotiate.WithView("silent-renew");
        }
    }
}