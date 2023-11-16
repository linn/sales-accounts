namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateOnBoardingActivity : SalesAccountActivity
    {
        public SalesAccountUpdateOnBoardingActivity(string updatedByUri, bool onBoardingAccount) : base(updatedByUri)
        {
            this.OnBoardingAccount = onBoardingAccount;
        }

        private SalesAccountUpdateOnBoardingActivity()
        {
            // ef
        }

        public bool OnBoardingAccount { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
