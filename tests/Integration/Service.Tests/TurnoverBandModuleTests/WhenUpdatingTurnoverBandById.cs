﻿namespace Linn.SalesAccounts.Service.Tests.TurnoverBandModuleTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;
    using Linn.SalesAccounts.Resources;
    using Linn.SalesAccounts.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingTurnoverBandById : ContextBase
    {
        private ProposedTurnoverBand proposedTurnoverBand;

        private ProposedTurnoverBandUpdateResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ProposedTurnoverBandUpdateResource { TurnoverBandUri = "/tb/12" };
            this.proposedTurnoverBand = new ProposedTurnoverBand
                                            {
                                                SalesAccount = new SalesAccount(new SalesAccountCreateActivity("/employees/100", 1, "one")),
                                                ProposedTurnoverBandUri = "/tb/12"
                                            };
            this.TurnoverBandService.OverrideTurnoverBand(88, "/tb/12")
                .Returns(new SuccessResult<ProposedTurnoverBand>(this.proposedTurnoverBand));
            this.Response = this.Browser.Put(
                "/sales/accounts/turnover-band-proposals/details/88",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.TurnoverBandService.Received(1).OverrideTurnoverBand(88, this.requestResource.TurnoverBandUri);
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
            resource.ProposedTurnoverBandUri.Should().Be("/tb/12");
        }
    }
}