﻿namespace Linn.SalesAccounts.Domain
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
            this.OnBoardingAccount = false;
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

        public bool OnBoardingAccount { get; set; }

        public void CloseAccount(SalesAccountCloseActivity closeAccountActivity)
        {
            this.ClosedOn = closeAccountActivity.ClosedOn;

            this.Activities.Add(closeAccountActivity);
        }

        public void ReopenAccount(SalesAccountReopenActivity closeAccountActivity)
        {
            if (!this.ClosedOn.HasValue)
            {
                return;
            }

            this.ClosedOn = null;
            this.Activities.Add(closeAccountActivity);
        }

        public void UpdateAccount(
            string updatedByUri,
            DiscountScheme discountScheme,
            string turnoverBandUri,
            bool eligibleForGoodCredit,
            bool eligibleForRebate,
            bool growthPartner,
            bool onBoardingAccount)
        {
            this.CheckUpdate(discountScheme, turnoverBandUri);

            if (discountScheme?.DiscountSchemeUri != this.DiscountSchemeUri)
            {
                this.UpdateDiscountScheme(new SalesAccountUpdateDiscountSchemeUriActivity(updatedByUri, discountScheme?.DiscountSchemeUri));
            }

            if (turnoverBandUri != this.TurnoverBandUri)
            {
                this.UpdateTurnoverBand(new SalesAccountUpdateTurnoverBandUriActivity(updatedByUri, turnoverBandUri));
            }

            if (eligibleForGoodCredit != this.EligibleForGoodCreditDiscount)
            {
                this.UpdateGoodCredit(new SalesAccountUpdateGoodCreditActivity(updatedByUri, eligibleForGoodCredit));
            }

            if (eligibleForRebate != this.EligibleForRebate)
            {
                this.UpdateRebate(new SalesAccountUpdateRebateActivity(updatedByUri, eligibleForRebate));
            }

            if (growthPartner != this.GrowthPartner)
            {
                this.UpdateGrowthPartner(new SalesAccountGrowthPartnerActivity(updatedByUri, growthPartner));
            }

            if (onBoardingAccount != this.OnBoardingAccount)
            {
                this.UpdateOnBoardingAccount(new SalesAccountUpdateOnBoardingActivity(updatedByUri, onBoardingAccount));
            }
        }

        public void UpdateNameAndAddress(string updatedByUri, string name, SalesAccountAddress address)
        {
            if (name != this.Name)
            {
                this.UpdateName(new SalesAccountUpdateNameActivity(updatedByUri, name));
            }

            if (address == null)
            {
                return;
            }

            if (address.Line1 != this.Address?.Line1
                || address.Line2 != this.Address?.Line2
                || address.Line3 != this.Address?.Line3
                || address.Line4 != this.Address?.Line4
                || address.CountryUri != this.Address?.CountryUri
                || address.Postcode != this.Address?.Postcode)
            {
                this.UpdateAddress(new SalesAccountUpdateAddressActivity(updatedByUri, address));
            }
        }

        public void UpdateGrowthPartner(SalesAccountGrowthPartnerActivity growthPartnerActivity)
        {
            this.GrowthPartner = growthPartnerActivity.GrowthPartner;
            this.Activities.Add(growthPartnerActivity);
        }

        public void UpdateOnBoardingAccount(SalesAccountUpdateOnBoardingActivity onBoardingActivity)
        {
            this.OnBoardingAccount = onBoardingActivity.OnBoardingAccount;
            this.Activities.Add(onBoardingActivity);
        }

        public void ApplyTurnoverBandProposal(string updatedByUri, ProposedTurnoverBand proposedTurnoverBand)
        {
            var activity = new SalesAccountApplyTurnoverBandProposalActivity(
                updatedByUri,
                proposedTurnoverBand.ProposedTurnoverBandUri,
                proposedTurnoverBand.FinancialYear);
            this.Activities.Add(activity);
            this.TurnoverBandUri = activity.TurnoverBandUri;
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
