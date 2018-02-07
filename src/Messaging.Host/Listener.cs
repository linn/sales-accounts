namespace Linn.SalesAccounts.Messaging.Host
{
    using System;
    using System.Threading;

    using Autofac;

    using Linn.Common.Logging;
    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Messaging.RabbitMQ.Unicast;
    using Linn.SalesAccounts.Messaging.Handlers;

    public class Listener
    {
        private readonly IReceiver receiver;
        private readonly DedupingMessageConsumer consumer;
        private readonly ILog logger;

        public Listener(ILifetimeScope scope, ILog logger)
        {
            this.logger = logger;
            this.receiver = scope.Resolve<IReceiver>();
            this.consumer = new DedupingMessageConsumer(new MessageConsumer(this.receiver), this.receiver);

            this.logger.Info("Started sales accounts listener");

            this.consumer.For("linnapps.sales-account.created")
                .OnConsumed(m =>
                    {
                        using (var handlerScope = scope.BeginLifetimeScope("messageHandler"))
                        {
                            var handler = handlerScope.Resolve<SalesAccountCreatedHandler>();
                            return handler.Execute(m);
                        }
                    })
                .OnRejected(this.LogRejection);

            this.consumer.For("linnapps.sales-account.updated")
                .OnConsumed(m =>
                    {
                        using (var handlerScope = scope.BeginLifetimeScope("messageHandler"))
                        {
                            var handler = handlerScope.Resolve<SalesAccountUpdatedHandler>();
                            return handler.Execute(m);
                        }
                    })
                .OnRejected(this.LogRejection);
        }

        public void Listen()
        {
            try
            {
                var message = this.receiver.Receive(5000);
                this.consumer.Consume(message);
            }
            catch (Exception e)
            {
                this.logger.Error("Logger Exception " + e.Message);
                Thread.Sleep(1000);
            }
        }

        private void LogRejection(IReceivedMessage message)
        {
            Console.WriteLine($"The message with Id {message.MessageId} was rejected.");
            this.logger.Error($"The message with Id {message.MessageId} was rejected.");
        }
    }
}
