namespace Linn.SalesAccounts.Resources.Messaging
{
    public class LinnappsSalesAccountResource
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string AccountType { get; set; }

        public string Currency { get; set; }

        public string DateClosed { get; set; }

        public int DefaultPaymentDays { get; set; }

        public int LedgerId { get; set; }

        public bool OnHold { get; set; }

        public string OrderComments { get; set; }

        public string PaymentType { get; set; }

        public AddressResource AccountAddress { get; set; }
    }
}