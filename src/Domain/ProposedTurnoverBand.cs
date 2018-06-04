namespace Linn.SalesAccounts.Domain
{
    public class ProposedTurnoverBand : Entity
    {
        public ProposedTurnoverBand()
        {
            this.IncludeInUpdate = true;
        }

        public SalesAccount SalesAccount { get; set; }

        public string FinancialYear { get; set; }

        public decimal SalesValueBase { get; set; }

        public decimal SalesValueCurrency { get; set; }

        public string CurrencyCode { get; set; }

        public string CalculatedTurnoverBandUri { get; set; }

        public string ProposedTurnoverBandUri { get; set; }

        public bool IncludeInUpdate { get; private set; }

        public bool AppliedToAccount { get; set; }

        public void OverrideProposedTurnoverBand(string turnoverBandUri)
        {
            if (this.AppliedToAccount)
            {
                return;
            }

            this.ProposedTurnoverBandUri = turnoverBandUri;
            this.IncludeProposalInUpdate();
        }

        public void IncludeProposalInUpdate()
        {
            this.IncludeInUpdate = true;
        }

        public void ExcludeFromUpdate()
        {
            if (!this.AppliedToAccount)
            {
                this.IncludeInUpdate = false;
            }
        }

        public void ApplyTurnoverBandToAccount()
        {
            this.SalesAccount.ApplyTurnoverBandProposal(this);
            this.AppliedToAccount = true;
        }
    }
}
