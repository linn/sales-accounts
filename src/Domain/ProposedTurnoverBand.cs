namespace Linn.SalesAccounts.Domain
{
    public class ProposedTurnoverBand : Entity
    {
        public SalesAccount SalesAccount { get; set; }

        public string FinancialYear { get; set; }

        public decimal SalesValueBase { get; set; }

        public decimal SalesValueCurrency { get; set; }

        public string CalculatedTurnoverBandUri { get; set; }

        public string ProposedTurnoverBandUri { get; set; }

        public bool IncludeInUpdate { get; set; }

        public bool AppliedToAccount { get; set; }

        public void OverrideProposedTurnoverBand(string turnoverBandUri)
        {
            this.ProposedTurnoverBandUri = turnoverBandUri;
        }

        public void ApplyTurnoverBandToAccount()
        {
            this.SalesAccount.ApplyTurnoverBandProposal(this);
            this.AppliedToAccount = true;
        }
    }
}
