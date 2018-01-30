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

        public int AccountId { get; private set; }

        public int OutletNumber { get; private set; }

        public string Name { get; private set; }

        public DateTime? ClosedOn { get; private set; }

        public bool EligibleForGoodCreditDiscount { get; set; }

        public string TurnoverBandUri { get; set; }

        public string DiscountSchemeUri { get; set; }

        public void CloseAccount(SalesAccountCloseActivity closeAccountActivity)
        {
            this.ClosedOn = closeAccountActivity.ClosedOn;

            this.Activities.Add(closeAccountActivity);
        }

        public void UpdateAccount(string discountSchemeUri, string turnoverBandUri, bool eligibleForGoodCredit)
        {
            if (discountSchemeUri != this.DiscountSchemeUri)
            {
                this.UpdateDiscountScheme(new SalesAccountUpdateDiscountSchemeUriActivity(discountSchemeUri));
            }

            if (turnoverBandUri != this.TurnoverBandUri)
            {
                this.UpdateTurnoverBand(new SalesAccountUpdateTurnoverBandUriActivity(turnoverBandUri));
            }

            if (eligibleForGoodCredit != this.EligibleForGoodCreditDiscount)
            {
                this.UpdateGoodCredit(new SalesAccountUpdateGoodCreditActivity(eligibleForGoodCredit));
            }
        }

        private void UpdateDiscountScheme(SalesAccountUpdateDiscountSchemeUriActivity updateActivity)
        {
            this.DiscountSchemeUri = updateActivity.DiscountSchemeUri;
            this.Activities.Add(updateActivity);
        }

        private void UpdateGoodCredit(SalesAccountUpdateGoodCreditActivity updateActivity)
        {
            this.EligibleForGoodCreditDiscount = updateActivity.EligibleForGoodCreditDiscount;
            this.Activities.Add(updateActivity);
        }

        private void UpdateTurnoverBand(SalesAccountUpdateTurnoverBandUriActivity updateActivity)
        {
            this.TurnoverBandUri = updateActivity.TurnoverBandUri;
            this.Activities.Add(updateActivity);
        }
    }
}
