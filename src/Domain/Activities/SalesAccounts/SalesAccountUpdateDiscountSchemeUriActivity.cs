namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateDiscountSchemeUriActivity : SalesAccountActivity
    {
        public SalesAccountUpdateDiscountSchemeUriActivity(string updatedByUri, string discountSchemeUri) : base(updatedByUri)
        {
            this.DiscountSchemeUri = discountSchemeUri;
        }

        private SalesAccountUpdateDiscountSchemeUriActivity()
        {
            // ef
        }

        public string DiscountSchemeUri { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}