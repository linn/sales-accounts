namespace Linn.SalesAccounts.Domain
{
    using System;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    public class SalesAccount : ActivityEntity<SalesAccountActivity>
    {
        public SalesAccount(SalesAccountCreateActivity createActivity)
        {
            this.AccountId = createActivity.AccountId;
            this.OutletNumber = createActivity.OutletNumber;
            this.Name = createActivity.Name;
            this.ClosedOn = createActivity.ClosedOn;

            this.Activities.Add(createActivity);
        }

        private SalesAccount()
        {
            // ef
        }

        public int AccountId { get; }

        public int OutletNumber { get; }

        public string Name { get; }

        public DateTime? ClosedOn { get; private set; }

        public bool EligibleForPromptPayDiscount { get; set; }

        public string TurnoverBandUri { get; set; }

        public string DiscountSchemeUri { get; set; }

        public void CloseAccount(SalesAccountCloseActivity closeAccountActivity)
        {
            this.ClosedOn = closeAccountActivity.ClosedOn;

            this.Activities.Add(closeAccountActivity);
        }
    }
}
