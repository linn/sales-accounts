namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateAddressActivity : SalesAccountActivity
    {
        public SalesAccountUpdateAddressActivity(SalesAccountAddress address)
        {
            this.Address = address;
        }

        private SalesAccountUpdateAddressActivity()
        {
            // ef
        }

        public SalesAccountAddress Address { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}