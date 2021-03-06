﻿namespace Linn.SalesAccounts.Facade.Tests.TurnoverBandServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Domain.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCalculatingTurnoverBands : ContextBase
    {
        private string financialYear;

        [SetUp]
        public void SetUp()
        {
            var account1 = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "one"));
            var account2 = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 2, "two"));
            this.financialYear = "2018/19";
            this.ProposedTurnoverBandService.CalculateProposedTurnoverBands(this.financialYear)
                .Returns(new TurnoverBandProposal(
                    this.financialYear,
                    new List<ProposedTurnoverBand>
                        {
                            new ProposedTurnoverBand { SalesAccount = account1, FinancialYear = this.financialYear, CalculatedTurnoverBandUri = "/tb/1" },
                            new ProposedTurnoverBand { SalesAccount = account2, FinancialYear = this.financialYear, CalculatedTurnoverBandUri = "/tb/2" }
                        }));
            this.Results = this.Sut.ProposeTurnoverBands(this.financialYear);
        }

        [Test]
        public void ShouldGetResultsFromService()
        {
            this.ProposedTurnoverBandService.Received().CalculateProposedTurnoverBands(this.financialYear);
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldReturnSuccessResults()
        {
            this.Results.Should().BeOfType<SuccessResult<TurnoverBandProposal>>();
            var dataResults = ((SuccessResult<TurnoverBandProposal>)this.Results).Data.ProposedTurnoverBands.ToList();
            dataResults.Should().HaveCount(2);
            dataResults.First(a => a.SalesAccount.Id == 1).CalculatedTurnoverBandUri.Should().Be("/tb/1");
            dataResults.First(a => a.SalesAccount.Id == 2).CalculatedTurnoverBandUri.Should().Be("/tb/2");
        }
    }
}
