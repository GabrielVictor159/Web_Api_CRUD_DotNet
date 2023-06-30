using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.RemoveOrder.Handlers;

namespace gcsb.ecommerce.application.UseCases.Order.RemoveOrder
{
    public class RemoveOrderUseCase : IRemoveOrderRequest
    {
        private readonly IOutputPort<DeleteOrderOutput> outputPort;
        private readonly RemoveOrderHandler removeOrderHandler;
        public RemoveOrderUseCase(
         IOutputPort<DeleteOrderOutput> outputPort,
         RemoveOrderHandler removeOrderHandler)
        {
            this.outputPort = outputPort;
            this.removeOrderHandler = removeOrderHandler;
        }
        public async Task Execute(RemoveOrderRequest request)
        {
            try
           {
            await removeOrderHandler.ProcessRequest(request);
            outputPort.Standard(request.orderResult!);
           }
           catch(Exception ex)
           {
            outputPort.Error(ex.Message);
           }
        }
    }
}