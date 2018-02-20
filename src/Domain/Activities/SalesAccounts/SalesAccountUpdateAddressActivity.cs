namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateAddressActivity : SalesAccountActivity
    {
        public SalesAccountUpdateAddressActivity(string address)
        {
            this.Address = address;
        }

        private SalesAccountUpdateAddressActivity()
        {
            // ef
        }

        public string Address { get; private set; }
    }
}