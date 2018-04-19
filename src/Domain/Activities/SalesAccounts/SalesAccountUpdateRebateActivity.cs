namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateRebateActivity : SalesAccountActivity
    {
        public SalesAccountUpdateRebateActivity(bool eligibleForRebate)
        {
            this.EligibleForRebate = eligibleForRebate;
        }

        private SalesAccountUpdateRebateActivity()
        {
            // ef
        }

        public bool EligibleForRebate { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}