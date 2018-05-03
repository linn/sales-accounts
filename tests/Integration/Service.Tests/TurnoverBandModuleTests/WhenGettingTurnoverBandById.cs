﻿namespace Linn.SalesAccounts.Service.Tests.TurnoverBandModuleTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTurnoverBandById : ContextBase
    {
        private ProposedTurnoverBand proposedTurnoverBand;

        [SetUp]
        public void SetUp()
        {
            this.proposedTurnoverBand = new ProposedTurnoverBand
                                            {
                                                SalesAccount = new SalesAccount(new SalesAccountCreateActivity(1, "one")),
                                                CalculatedTurnoverBandUri = "/tb/1"
                                            };
            this.TurnoverBandService.GetProposedTurnoverBand(88).Returns(new SuccessResult<ProposedTurnoverBand>(this.proposedTurnoverBand));
            this.Response = this.Browser.Get(
                "/sales/accounts/proposed-turnover-bands/88",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnCorrectContentType()
        {
            this.Response.ContentType.Should().Be("application/vnd.linn.sales.account-proposed-turnover-band+json;version=1");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProposedTurnoverBandResource>();
            resource.SalesAccountId.Should().Be(1);
            resource.CalculatedTurnoverBandUri.Should().Be("/tb/1");
        }
    }
}