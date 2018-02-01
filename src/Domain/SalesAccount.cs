namespace Linn.SalesAccounts.Domain
{
    using System;
    using System.Linq;

    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Exceptions;
    using Linn.SalesAccounts.Domain.External;

    public class SalesAccount : ActivityEntity<SalesAccountActivity>
    {
        public SalesAccount(SalesAccountCreateActivity createActivity)
        {
            this.Id = createActivity.AccountId;
            this.Name = createActivity.Name;
            this.ClosedOn = createActivity.ClosedOn;
            this.Activities.Add(createActivity);
        }

        private SalesAccount()
        {
            // ef
        }
        
        public string Name { get; private set; }

        public DateTime? ClosedOn { get; private set; }

        public bool EligibleForGoodCreditDiscount { get; set; }

        public bool EligibleForRebate { get; set; }

        public string TurnoverBandUri { get; set; }

        public string DiscountSchemeUri { get; set; }

        public void CloseAccount(SalesAccountCloseActivity closeAccountActivity)
        {
            this.ClosedOn = closeAccountActivity.ClosedOn;

            this.Activities.Add(closeAccountActivity);
        }
        
        public void UpdateAccount(
            DiscountScheme discountScheme,
            string turnoverBandUri,
            bool eligibleForGoodCredit,
            bool eligibleForRebate)
        {
            this.CheckUpdate(discountScheme, turnoverBandUri);

            if (discountScheme.DiscountSchemeUri != this.DiscountSchemeUri)
            {
                this.UpdateDiscountScheme(new SalesAccountUpdateDiscountSchemeUriActivity(discountScheme.DiscountSchemeUri));
            }

            if (turnoverBandUri != this.TurnoverBandUri)
            {
                this.UpdateTurnoverBand(new SalesAccountUpdateTurnoverBandUriActivity(turnoverBandUri));
            }

            if (eligibleForGoodCredit != this.EligibleForGoodCreditDiscount)
            {
                this.UpdateGoodCredit(new SalesAccountUpdateGoodCreditActivity(eligibleForGoodCredit));
            }

            if (eligibleForRebate != this.EligibleForRebate)
            {
                this.UpdateRebate(new SalesAccountUpdateRebateActivity(eligibleForRebate));
            }
        }

        public void UpdateName(string name)
        {
            if (name != this.Name)
            {
                this.UpdateName(new SalesAccountUpdateNameActivity(name));
            }
        }

        private void CheckUpdate(DiscountScheme discountScheme, string turnoverBandUri)
        {
            if (!discountScheme.TurnoverBandUris.Contains(turnoverBandUri))
            {
                throw new InvalidTurnoverBandException($"Discount scheme {discountScheme.Name} does not contain turnover band {turnoverBandUri}");
            }
        }

        private void UpdateName(SalesAccountUpdateNameActivity updateActivity)
        {
            this.Name = updateActivity.Name;
            this.Activities.Add(updateActivity);
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

        private void UpdateRebate(SalesAccountUpdateRebateActivity updateActivity)
        {
            this.EligibleForRebate = updateActivity.EligibleForRebate;
            this.Activities.Add(updateActivity);
        }

        private void UpdateTurnoverBand(SalesAccountUpdateTurnoverBandUriActivity updateActivity)
        {
            this.TurnoverBandUri = updateActivity.TurnoverBandUri;
            this.Activities.Add(updateActivity);
        }
    }
}
