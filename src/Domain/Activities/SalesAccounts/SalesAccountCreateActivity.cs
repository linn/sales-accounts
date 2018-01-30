﻿namespace Linn.SalesAccounts.Domain.Activities.SalesAccounts
{
    using System;

    public class SalesAccountCreateActivity : SalesAccountActivity
    {
        public SalesAccountCreateActivity(int accountId, int outletNumber, string name, DateTime? closedOn = null)
        {
            this.AccountId = accountId;
            this.OutletNumber = outletNumber;
            this.Name = name;
            this.ClosedOn = closedOn;
        }

        private SalesAccountCreateActivity()
        {
            // ef
        }

        public int AccountId { get; private set; }

        public int OutletNumber { get; private set; }

        public string Name { get; private set; }

        public DateTime? ClosedOn { get; private set; }
    }
}