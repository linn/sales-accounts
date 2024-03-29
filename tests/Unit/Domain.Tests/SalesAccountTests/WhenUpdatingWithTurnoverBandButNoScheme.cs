﻿namespace Linn.SalesAccounts.Domain.Tests.SalesAccountTests
{
    using System;

    using FluentAssertions;

    using Linn.SalesAccounts.Domain.Exceptions;

    using NUnit.Framework;

    public class WhenUpdatingWithTurnoverBandButNoScheme : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.action = () => this.Sut.UpdateAccount("employees/100", null, "/tb/1", true, true, true, false);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.ShouldThrow<InvalidTurnoverBandException>();
        }
    }
}
