namespace Linn.SalesAccounts.Service.Modules
{
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Resources.RequestResources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class TurnoverBandModule : NancyModule
    {
        private readonly ITurnoverBandService turnoverBandService;

        public TurnoverBandModule(ITurnoverBandService turnoverBandService)
        {
            this.turnoverBandService = turnoverBandService;

            this.Get("/sales/accounts/proposed-turnover-bands", _ => this.GetProposedTurnoverBands());
            this.Post("/sales/accounts/proposed-turnover-bands", _ => this.SetProposedTurnoverBands());
        }

        private object SetProposedTurnoverBands()
        {
            var resource = this.Bind<ProposedTurnoverBandRequestResource>();
            var proposedTurnoverBands = this.turnoverBandService.ProposeTurnoverBands(resource.FinancialYear);
            return this.Negotiate.WithModel(proposedTurnoverBands);
        }

        private object GetProposedTurnoverBands()
        {
            var resource = this.Bind<ProposedTurnoverBandRequestResource>();
            throw new System.NotImplementedException();
        }
    }
}
