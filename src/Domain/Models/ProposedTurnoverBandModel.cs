namespace Linn.SalesAccounts.Domain.Models
{
    public class ProposedTurnoverBandModel
    {
        public int SalesAccountId { get; set; }

        public string SalesAccountName { get; set; }

        public string FinancialYear { get; set; }

        public decimal SalesValueBase { get; set; }

        public decimal SalesValueCurrency { get; set; }

        public string CurrentTurnoverBandName { get; set; }

        public string CalculatedTurnoverBandName { get; set; }

        public string ProposedTurnoverBandName { get; set; }

        public bool IncludeInUpdate { get; set; }

        public bool AppliedToAccount { get; set; }
    }
}
