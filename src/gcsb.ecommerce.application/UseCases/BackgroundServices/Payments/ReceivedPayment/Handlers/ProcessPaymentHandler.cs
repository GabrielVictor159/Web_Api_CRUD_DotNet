using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.BackgroundServices.Payments.ReceivedPayment.Handlers
{
    public class ProcessPaymentHandler : Handler<Boundaries.Messages.Payment.ReceivedPayment>
    {
        public readonly ReversalPaymentHandler reversalPaymentHandler;
        public readonly IOrderRepository orderRepository;
        public ProcessPaymentHandler(ReversalPaymentHandler reversalPaymentHandler, IOrderRepository orderRepository)
        {
           this.reversalPaymentHandler = reversalPaymentHandler; 
           this.orderRepository = orderRepository;
        }
        public override async Task ProcessRequest(Boundaries.Messages.Payment.ReceivedPayment request)
        {
           decimal ReversalAmount = 0;
            var order = await orderRepository.GetOrderByIdAsync(request.IdOrder);
            if(order == null)
            {
                ReversalAmount = request.AmountPaid;
            }
            else{
                if(order.TotalOrder>request.AmountPaid)
                {
                    ReversalAmount = order.TotalOrder;
                }
                else if(order.TotalOrder<request.AmountPaid)
                {
                    ReversalAmount =request.AmountPaid-order.TotalOrder;
                    order.IdPayment = request.IdPayment;
                    await orderRepository.UpdateAsync(order);
                }
                else if(order.TotalOrder==request.AmountPaid)
                {
                    order.IdPayment = request.IdPayment;
                    await orderRepository.UpdateAsync(order);
                }
            }
            if(ReversalAmount > 0)
            {
               await reversalPaymentHandler.ProcessRequest(
                new Boundaries.Messages.Payment.ReversalPayment()
                {
                    IdPayment=request.IdPayment,
                    ReversedAmount=ReversalAmount
                }
                );
            }
        }
    }
}