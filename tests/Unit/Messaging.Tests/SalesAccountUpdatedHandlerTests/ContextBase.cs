﻿namespace Linn.SalesAccounts.Messaging.Tests.SalesAccountUpdatedHandlerTests
{
    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Persistence;
    using Linn.SalesAccounts.Facade.Services;
    using Linn.SalesAccounts.Messaging.Handlers;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected SalesAccountUpdatedHandler Sut { get; set; }

        protected ISalesAccountService SalesAccountService { get; set; }

        protected ITransactionManager TransactionManager { get; set; }

        protected IRabbitTerminator RabbitTerminator { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.SalesAccountService = Substitute.For<ISalesAccountService>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.RabbitTerminator = Substitute.For<IRabbitTerminator>();
            this.Sut = new SalesAccountUpdatedHandler(this.SalesAccountService, this.TransactionManager, this.RabbitTerminator);
        }
    }
}