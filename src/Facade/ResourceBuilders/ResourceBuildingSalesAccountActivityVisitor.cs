namespace Linn.SalesAccounts.Facade.ResourceBuilders
{
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources.SalesAccounts;

    public class ResourceBuildingSalesAccountActivityVisitor : ISalesAccountActivityVisitor<SalesAccountActivityResource>
    {
        public SalesAccountActivityResource Visit(SalesAccountCloseActivity activity)
        {
            return new SalesAccountCloseActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           ClosedOn = activity.ClosedOn.ToString("o")
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountCreateActivity activity)
        {
            return new SalesAccountCreateActivityResource
                       {
                           AccountId = activity.AccountId,
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           ClosedOn = activity.ClosedOn?.ToString("o"),
                           Name = activity.Name
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountGrowthPartnerActivity activity)
        {
            return new SalesAccountGrowthPartnerActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           GrowthPartner = activity.GrowthPartner
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountUpdateAddressActivity activity)
        {
            return new SalesAccountUpdateAddressActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           Line1 = activity.Address?.Line1,
                           Line2 = activity.Address?.Line2,
                           Line3 = activity.Address?.Line3,
                           Line4 = activity.Address?.Line4,
                           CountryUri = activity.Address?.CountryUri,
                           Postcode = activity.Address?.Postcode
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountUpdateDiscountSchemeUriActivity activity)
        {
            return new SalesAccountUpdateDiscountSchemeUriActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           DiscountSchemeUri = activity.DiscountSchemeUri
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountUpdateGoodCreditActivity activity)
        {
            return new SalesAccountUpdateGoodCreditActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           EligibleForGoodCreditDiscount = activity.EligibleForGoodCreditDiscount
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountUpdateNameActivity activity)
        {
            return new SalesAccountUpdateNameActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           Name = activity.Name
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountUpdateRebateActivity activity)
        {
            return new SalesAccountUpdateRebateActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           EligibleForRebate = activity.EligibleForRebate
                       };
        }

        public SalesAccountActivityResource Visit(SalesAccountUpdateTurnoverBandUriActivity activity)
        {
            return new SalesAccountUpdateTurnoverBandUriActivityResource
                       {
                           ActivityType = activity.GetType().Name,
                           ChangedOn = activity.ChangedOn.ToString("o"),
                           TurnoverBandUri = activity.TurnoverBandUri
                       };
        }
    }
}