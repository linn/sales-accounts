namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateNameActivity : SalesAccountActivity
    {
        public SalesAccountUpdateNameActivity(string updatedByUri, string name) : base(updatedByUri)
        {
            this.Name = name;
        }

        private SalesAccountUpdateNameActivity()
        {
            // ef
        }

        public string Name { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}