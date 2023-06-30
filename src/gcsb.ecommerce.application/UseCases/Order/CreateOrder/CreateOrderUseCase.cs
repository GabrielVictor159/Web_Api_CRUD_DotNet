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
        private readonly ValidateProductsHandler validateProductsHandler;
        private readonly IOutputPort<CreateOrderOutput> outputPort;
        public CreateOrderUseCase(
         ValidateProductsHandler validateProductsHandler,
         CreateOrderDomainHandler createOrderDomainHandler,
         ValidateOrderHandler validateOrderHandler,
         SaveOrderHandler saveOrderHandler,
         IOutputPort<CreateOrderOutput> outputPort)
        {
        validateOrderHandler.SetSucessor(saveOrderHandler);
        createOrderDomainHandler.SetSucessor(validateOrderHandler);
        validateProductsHandler.SetSucessor(createOrderDomainHandler);
        this.validateProductsHandler = validateProductsHandler;
        this.outputPort = outputPort;
       
        }
        public async Task Execute(CreateOrderRequest request)
        {
           try
           {
            await validateProductsHandler.ProcessRequest(request);
            outputPort.Standard(request.OrderOutput!);
           }
           catch(Exception ex)
           {
            outputPort.Error(ex.Message);
           }
        }
    }
}