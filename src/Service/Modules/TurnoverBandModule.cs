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
            this.Get("/sales/accounts/proposed-turnover-bands/{id:int}", parameters => this.GetProposedTurnoverBand(parameters.id));
            this.Put("/sales/accounts/proposed-turnover-bands/{id:int}", parameters => this.UpdateProposedTurnoverBand(parameters.id));
            this.Post("/sales/accounts/proposed-turnover-bands", _ => this.SetProposedTurnoverBands());
        }

        private object GetProposedTurnoverBand(int id)
        {
            return this.Negotiate.WithModel(this.turnoverBandService.GetProposedTurnoverBand(id));
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
            var proposedTurnoverBands = this.turnoverBandService.GetProposedTurnoverBands(resource.FinancialYear);
            return this.Negotiate.WithModel(proposedTurnoverBands);
        }

        private object UpdateProposedTurnoverBand(int id)
        {
            var resource = this.Bind<ProposedTurnoverBandUpdateResource>();
            var turnoverBand = this.turnoverBandService.OverrideTurnoverBand(id, resource.TurnoverBandUri);
            return this.Negotiate.WithModel(turnoverBand);
        }
    }
}
