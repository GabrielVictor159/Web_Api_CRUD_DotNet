using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Messages;
using gcsb.ecommerce.application.UseCases.BackgroundServices.Payments.ReceivedPayment.Handlers;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace gcsb.ecommerce.application.UseCases.BackgroundServices.Payments.ReceivedPayment
{
    public class ReceivedPaymentUseCase : BackgroundService
    {
        public readonly ProcessPaymentHandler processPayment;
        public readonly IMessageReceiver messageReceiver;
        public ReceivedPaymentUseCase(
            ProcessPaymentHandler processPayment,
            IMessageReceiver MessageReceiver
        )
        {
            this.processPayment = processPayment;
            this.messageReceiver = MessageReceiver;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
            messageReceiver.ReceiveFanoutExchangeMessages("Payment_Received","Orders",
            message => {
                processPayment?.ProcessRequest(
                    JsonConvert.DeserializeObject<Boundaries.Messages.Payment.ReceivedPayment>(message)!
                    );
                return true;
            });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

    }
}
