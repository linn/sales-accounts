namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public interface ISalesAccountActivityVisitor<out T>
    {
        T Visit(SalesAccountCloseActivity activity);

        T Visit(SalesAccountReopenActivity activity);

        T Visit(SalesAccountCreateActivity activity);

        T Visit(SalesAccountGrowthPartnerActivity activity);

        T Visit(SalesAccountUpdateAddressActivity activity);

        T Visit(SalesAccountUpdateDiscountSchemeUriActivity activity);

        T Visit(SalesAccountUpdateGoodCreditActivity activity);

        T Visit(SalesAccountUpdateNameActivity activity);

        T Visit(SalesAccountUpdateRebateActivity activity);

        T Visit(SalesAccountUpdateTurnoverBandUriActivity activity);

        T Visit(SalesAccountApplyTurnoverBandProposalActivity activity);

        T Visit(SalesAccountUpdateOnBoardingActivity activity);
    }
}