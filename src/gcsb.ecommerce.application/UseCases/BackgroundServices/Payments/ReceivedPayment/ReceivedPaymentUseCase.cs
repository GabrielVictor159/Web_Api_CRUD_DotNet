using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Messages;
using gcsb.ecommerce.application.UseCases.BackgroundServices.Payments.ReceivedPayment.Handlers;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace gcsb.ecommerce.application.UseCases.BackgroundServices.Payments.ReceivedPayment
{
    public class ReceivedPaymentUseCase : BackgroundService
    {
        public readonly ProcessPaymentHandler processPayment;
        public ReceivedPaymentUseCase(
            ProcessPaymentHandler processPayment 
        )
        {
            this.processPayment = processPayment;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var messageReceiver = MessageReceiver<Boundaries.Messages.Payment.ReceivedPayment>.Builder()
                .WithQueueName("Orders")
                .WithExchangeName("Payment_Received")
                .WithExchangeType(ExchangeType.Fanout)
                .WithRoutingKey("") 
                .WithMessageHandler( message =>
                {
                    processPayment.ProcessRequest(message).Wait();
                    return true;
                })
                .Build();

            messageReceiver.StartReceivingMessages();

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

    }
}
