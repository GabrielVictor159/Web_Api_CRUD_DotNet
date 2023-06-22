using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Order.GetOrder.Handlers
{
    public class GetOrderHandler : Handler<GetOrderRequest>
    {
         private readonly IOrderRepository _orderRepository;
         public GetOrderHandler(IOrderRepository orderRepository)
         {
            this._orderRepository = orderRepository;
         }
        public override async Task ProcessRequest(GetOrderRequest request)
        {
           var result = await _orderRepository.GetOrder(request.func);
           request.SetOutput(result);
           sucessor?.ProcessRequest(request);
        }
    }
}