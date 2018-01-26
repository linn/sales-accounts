namespace Linn.SalesAccounts.Service.Modules
{
    using Nancy;
    using Nancy.Responses;

    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get("/", args => new RedirectResponse("/sales/accounts"));
            this.Get("/sales", args => new RedirectResponse("/sales/accounts"));
            this.Get("/sales/accounts", _ => this.GetApp());
            this.Get("/sales/accounts/(.*)", _ => this.GetApp());
        }

        private object GetApp()
        {
            return this.Negotiate.WithView("Index");
        }
    }
}