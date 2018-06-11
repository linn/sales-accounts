namespace Linn.SalesAccounts.Domain.Tests.ProposedTurnoverBandTests
{
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ProposedTurnoverBand Sut { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new ProposedTurnoverBand
                           {
                               CalculatedTurnoverBandUri = "/tb/1",
                               ProposedTurnoverBandUri = "/tb/1",
                               FinancialYear = "2018/19",
                               Id = 1,
                               SalesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "one")),
                               SalesValueCurrency = 1m,
                               SalesValueBase = 1m
            };
        }
    }
}