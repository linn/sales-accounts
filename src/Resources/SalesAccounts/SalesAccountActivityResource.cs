namespace Linn.SalesAccounts.Resources.SalesAccounts
{
    using Linn.Common.Resources;

    public class SalesAccountActivityResource : HypermediaResource
    {
        public string ActivityType { get; set; }

        public string ChangedOn { get; set; }
    }
}