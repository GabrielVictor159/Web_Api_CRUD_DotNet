using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Order.CreateOrder
{
    public sealed class OrderResponse
    {
        public Guid OrderId {get; private set;}
        public OrderResponse(Guid OrderId)
        {
            this.OrderId = OrderId;
        }
    }
}