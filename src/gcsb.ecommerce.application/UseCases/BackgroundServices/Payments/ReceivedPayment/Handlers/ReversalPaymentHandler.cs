using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Messages.Payment;
using gcsb.ecommerce.application.Messages;
using RabbitMQ.Client;

namespace gcsb.ecommerce.application.UseCases.BackgroundServices.Payments.ReceivedPayment.Handlers
{
    public class ReversalPaymentHandler : Handler<ReversalPayment>
    {
        public override async Task ProcessRequest(ReversalPayment request)
        {
           var messageSender = MessageSender<ReversalPayment>.Builder()
             .WithExchangeName("Payment_Reverse")
             .WithExchangeType(ExchangeType.Fanout)
             .Build();
           await Task.Run(() => messageSender.SendMessage(request));
            
        }
    }
}