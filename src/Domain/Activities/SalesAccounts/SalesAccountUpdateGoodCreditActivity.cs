﻿namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    public class SalesAccountUpdateGoodCreditActivity : SalesAccountActivity
    {
        public SalesAccountUpdateGoodCreditActivity(string updatedByUri, bool eligibleForGoodCreditDiscount) : base(updatedByUri)
        {
            this.EligibleForGoodCreditDiscount = eligibleForGoodCreditDiscount;
        }

        private SalesAccountUpdateGoodCreditActivity()
        {
            // ef
        }

        public bool EligibleForGoodCreditDiscount { get; private set; }

        public override T Accept<T>(ISalesAccountActivityVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}