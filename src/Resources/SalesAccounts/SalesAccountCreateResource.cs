namespace Linn.SalesAccounts.Resources.SalesAccounts
{
    public class SalesAccountCreateResource
    {
        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public string Name { get; set; }

        public string ClosedOn { get; set; }
    }
}
