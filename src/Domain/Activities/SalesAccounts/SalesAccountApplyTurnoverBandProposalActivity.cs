namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountApplyTurnoverBandProposalActivity : SalesAccountUpdateTurnoverBandUriActivity
    {
        public SalesAccountApplyTurnoverBandProposalActivity(string turnoverBandUri, string financialYear)
            : base(turnoverBandUri)
        {
            this.BasedOnFinancialYear = financialYear;
        }

        private SalesAccountApplyTurnoverBandProposalActivity()
        {
            // ef
        }

        public string BasedOnFinancialYear { get; set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}