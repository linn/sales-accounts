namespace Linn.SalesAccounts.Resources.SalesAccounts
{
    public class SalesAccountCreateActivityResource : SalesAccountActivityResource
    {
        public int AccountId { get; set; }

        public string Name { get; set; }

        public string ClosedOn { get; set; }
    }
}
