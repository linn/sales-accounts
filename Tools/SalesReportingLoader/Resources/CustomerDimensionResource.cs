namespace Linn.SalesAccounts.SalesReportingLoader.Resources
{
    public class CustomerDimensionResource
    {
        public virtual string CustomerName { get; set; }

        public virtual int SalesAccountId { get; set; }

        public virtual int OutletNumber { get; set; }

        public virtual bool GrowthPartner { get; set; }
    }
}
