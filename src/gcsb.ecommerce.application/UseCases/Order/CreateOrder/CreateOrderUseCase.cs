using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder
{
    public class CreateOrderUseCase : ICreateOrderRequest
    {
        private readonly ValidateOrderHandler validateOrderHandler;
        private readonly IOutputPort<OrderOutput> outputPort;
        public CreateOrderUseCase(
         ValidateOrderHandler validateOrderHandler,
         SaveOrderHandler saveOrderHandler,
         IOutputPort<OrderOutput> outputPort)
        {
        validateOrderHandler.SetSucessor(saveOrderHandler);
        this.validateOrderHandler = validateOrderHandler;
        this.outputPort = outputPort;
       
        }
        public async Task Execute(CreateOrderRequest request)
        {
           try
           {
            await validateOrderHandler.ProcessRequest(request);
            outputPort.Standard(new OrderOutput(request.Order.Id));
           }
           catch(Exception ex)
           {
            outputPort.Error(ex.Message);
           }
        }
    }
}