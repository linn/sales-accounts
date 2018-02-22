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

        public bool GrowthPartner { get; set; }

        public string TurnoverBandUri { get; set; }

        public string DiscountSchemeUri { get; set; }

        public SalesAccountAddress Address { get; set; }

        public void CloseAccount(SalesAccountCloseActivity closeAccountActivity)
        {
            this.ClosedOn = closeAccountActivity.ClosedOn;

            this.Activities.Add(closeAccountActivity);
        }

        public void UpdateAccount(
            DiscountScheme discountScheme,
            string turnoverBandUri,
            bool eligibleForGoodCredit,
            bool eligibleForRebate,
            bool growthPartner)
        {
            this.CheckUpdate(discountScheme, turnoverBandUri);

            if (discountScheme?.DiscountSchemeUri != this.DiscountSchemeUri)
            {
                this.UpdateDiscountScheme(new SalesAccountUpdateDiscountSchemeUriActivity(discountScheme?.DiscountSchemeUri));
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

            if (growthPartner != this.GrowthPartner)
            {
                this.UpdateGrowthPartner(new SalesAccountGrowthPartnerActivity(growthPartner));
            }
        }

        public void UpdateNameAndAddress(string name, SalesAccountAddress address)
        {
            if (name != this.Name)
            {
                this.UpdateName(new SalesAccountUpdateNameActivity(name));
            }

            if (!address.Line1.Equals(Address?.Line1)
                || !address.Line2.Equals(Address?.Line2)
                || !address.Line3.Equals(Address?.Line3)
                || !address.Line4.Equals(Address?.Line4)
                || !address.CountryUri.Equals(Address?.CountryUri)
                || !address.Postcode.Equals(Address?.Postcode))
            {
                this.UpdateAddress(new SalesAccountUpdateAddressActivity(address));
            }
        }

        public void UpdateGrowthPartner(SalesAccountGrowthPartnerActivity growthPartnerActivity)
        {
            this.GrowthPartner = growthPartnerActivity.GrowthPartner;
            this.Activities.Add(growthPartnerActivity);
        }

        private void CheckUpdate(DiscountScheme discountScheme, string turnoverBandUri)
        {
            if (string.IsNullOrEmpty(turnoverBandUri))
            {
                return;
            }

            if (discountScheme == null)
            {
                throw new InvalidTurnoverBandException($"Cannot use turnover band {turnoverBandUri} as no discount scheme specified");
            }

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

        private void UpdateAddress(SalesAccountUpdateAddressActivity updateActivity)
        {
            this.Address = updateActivity.Address;
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
