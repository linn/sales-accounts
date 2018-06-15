namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountGrowthPartnerActivity : SalesAccountActivity
    {
        public SalesAccountGrowthPartnerActivity(string updatedByUri, bool growthPartner) : base(updatedByUri)
        {
            this.GrowthPartner = growthPartner;
        }

        private SalesAccountGrowthPartnerActivity()
        {
            // ef
        }

        public bool GrowthPartner { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}