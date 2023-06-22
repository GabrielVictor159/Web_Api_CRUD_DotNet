using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder
{
    public class CreateOrderRequest
    {
       public domain.Order.Order Order {get;private set;}
       public OrderOutput? OrderOutput {get; private set;}
       public CreateOrderRequest(domain.Order.Order order)
       {
        Order = order;
       }

       public void SetOutput(Guid id)
        =>OrderOutput = new OrderOutput(id);
    }
}