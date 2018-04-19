namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateTurnoverBandUriActivity : SalesAccountActivity
    {
        public SalesAccountUpdateTurnoverBandUriActivity(string turnoverBandUri)
        {
            this.TurnoverBandUri = turnoverBandUri;
        }

        private SalesAccountUpdateTurnoverBandUriActivity()
        {
            // ef
        }

        public string TurnoverBandUri { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}