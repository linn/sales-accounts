namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountGrowthPartnerActivity : SalesAccountActivity
    {
        public SalesAccountGrowthPartnerActivity(bool growthPartner)
        {
            this.GrowthPartner = growthPartner;
        }

        private SalesAccountGrowthPartnerActivity()
        {
            // ef
        }

        public bool GrowthPartner { get; private set; }
    }
}