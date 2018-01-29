namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateDiscountSchemeUriActivity : SalesAccountActivity
    {
        public SalesAccountUpdateDiscountSchemeUriActivity(string discountSchemeUri)
        {
            this.DiscountSchemeUri = discountSchemeUri;
        }

        private SalesAccountUpdateDiscountSchemeUriActivity()
        {
            // ef
        }

        public string DiscountSchemeUri { get; set; }
    }
}