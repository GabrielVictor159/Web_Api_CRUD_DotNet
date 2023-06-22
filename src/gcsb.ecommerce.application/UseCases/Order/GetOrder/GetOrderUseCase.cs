using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers;
using gcsb.ecommerce.application.UseCases.Order.GetOrder.Handlers;

namespace gcsb.ecommerce.application.UseCases.Order.GetOrder
{
    public class GetOrderUseCase : IGetOrderRequest
    {
        private readonly IOutputPort<List<domain.Order.Order>> outputPort;
        private readonly GetOrderHandler getOrderHandler;
        public GetOrderUseCase( 
         IOutputPort<List<domain.Order.Order>> outputPort,
         GetOrderHandler getOrderHandler)
        {
            this.outputPort = outputPort;
            this.getOrderHandler = getOrderHandler;
        }
        public async Task Execute(GetOrderRequest request)
        {
           try
           {
            await getOrderHandler.ProcessRequest(request);
            outputPort.Standard(request.orderResult!);
           }
           catch(Exception ex)
           {
            outputPort.Error(ex.Message);
           }
        }
    }
}