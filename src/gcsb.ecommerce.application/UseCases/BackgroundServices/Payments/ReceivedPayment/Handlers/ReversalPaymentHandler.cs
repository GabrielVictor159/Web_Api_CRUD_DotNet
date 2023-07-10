using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Messages.Payment;
using gcsb.ecommerce.application.Interfaces.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace gcsb.ecommerce.application.UseCases.BackgroundServices.Payments.ReceivedPayment.Handlers
{
    public class ReversalPaymentHandler : Handler<ReversalPayment>
    {
      public readonly IMessageSender messageSender;
      public ReversalPaymentHandler(
        IMessageSender messageSender
      )
      {
        this.messageSender = messageSender;
      }
        public override async Task ProcessRequest(ReversalPayment request)
        {
           await Task.Run(() => 
           messageSender.SendFanoutExchangeMessage(
            "Payment_Reverse",
            JsonConvert.SerializeObject(request)
            )
           );
        }
    }
}