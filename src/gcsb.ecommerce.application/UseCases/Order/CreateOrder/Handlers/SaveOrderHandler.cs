using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers
{
    public class SaveOrderHandler : Handler<CreateOrderRequest>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public SaveOrderHandler(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public override async Task ProcessRequest(CreateOrderRequest request)
        {
            var result = await _orderRepository.CreateAsync(request.Order!);
            var output = _mapper.Map<CreateOrderOutput>(result);
            output.ListOrderProduct = _mapper.Map<List<OrderProductOutput>>(result.ListOrderProduct);
            
            request.SetOutput(output);
            if(sucessor!=null)
            {
            await sucessor.ProcessRequest(request);
            }
        }
    }
}