using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder.Handlers
{
    public class UpdateOrderHandler : Handler<UpdateOrderRequest>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        public UpdateOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public override async Task ProcessRequest(UpdateOrderRequest request)
        {
           if(request.NewAttributesOrder!=null)
           {
            var newOrder = await orderRepository.UpdateAsync(request.NewAttributesOrder);
            request.SetOutput(mapper.Map<UpdateOrderOutput>(newOrder!));
           }
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}