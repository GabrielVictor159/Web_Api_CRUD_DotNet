using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers
{
    public class SaveOrderHandler : Handler<CreateOrderRequest>
    {
        private readonly IOrderRepository _orderRepository;
        public SaveOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public override async Task ProcessRequest(CreateOrderRequest request)
        {
            await _orderRepository.Add(request.Order);
            request.SetOutput(request.Order.Id);
            sucessor?.ProcessRequest(request);
        }
    }
}