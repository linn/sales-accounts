namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateTurnoverBandUriActivity : SalesAccountActivity
    {
        public SalesAccountUpdateTurnoverBandUriActivity(string updatedByUri, string turnoverBandUri) : base(updatedByUri)
        {
            this.TurnoverBandUri = turnoverBandUri;
        }

        protected SalesAccountUpdateTurnoverBandUriActivity()
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