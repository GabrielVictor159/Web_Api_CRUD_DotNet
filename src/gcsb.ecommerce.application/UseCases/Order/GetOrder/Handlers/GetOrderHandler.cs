using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Order.GetOrder.Handlers
{
    public class GetOrderHandler : Handler<GetOrderRequest>
    {
         private readonly IOrderRepository _orderRepository;
         private readonly IMapper mapper;
         public GetOrderHandler(IOrderRepository orderRepository, IMapper mapper)
         {
            this._orderRepository = orderRepository;
            this.mapper = mapper;
         }
        public override async Task ProcessRequest(GetOrderRequest request)
        {
           var result = await _orderRepository.GetOrderAsync(request.func,request.page,request.pageSize);
           var orderOutput = mapper.Map<List<OrderOutput>>(result);
           int index =0;
           foreach(var output in orderOutput)
           {
            output.ListOrderProduct = mapper.Map<List<OrderProductOutput>>(result[index].ListOrderProduct);
            index++;
           }
           request.SetOutput(orderOutput);
           sucessor?.ProcessRequest(request);
        }
    }
}