using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.UseCases.Order.UpdateOrder.Handlers;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder
{
    public class UpdateOrderUseCase : IUpdateOrderRequest
    {
        private readonly IOutputPort<UpdateOrderOutput> outputPort;
        private readonly ValidateProductsHandler validateProductsHandler;
        public UpdateOrderUseCase(
            IOutputPort<UpdateOrderOutput> output,
            ValidateProductsHandler validateProductsHandler,
            CreateListOrderProductHandler createListOrderProductHandler,
            ValidateOrderHandler validateOrderHandler,
            UpdateOrderHandler updateOrderHandler
        )
        {
            validateProductsHandler.SetSucessor(createListOrderProductHandler);
            createListOrderProductHandler.SetSucessor(validateOrderHandler);
            validateOrderHandler.SetSucessor(updateOrderHandler);
            this.validateProductsHandler = validateProductsHandler;
            this.outputPort = output;
        }
        public async Task Execute(UpdateOrderRequest request)
        {
           try
           {
            await validateProductsHandler.ProcessRequest(request);
            outputPort.Standard(request.orderResult!);
           }
           catch (Exception ex)
           {
             outputPort.Error(ex.Message);
           }
        }
    }
}