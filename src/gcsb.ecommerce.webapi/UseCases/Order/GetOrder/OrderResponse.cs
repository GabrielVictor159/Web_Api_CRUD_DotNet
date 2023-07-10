using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.webapi.UseCases.Order.GetOrder
{
    public class OrderResponse
    {
        public List<OrderOutput> orders {get;private set;}
        public OrderResponse(List<OrderOutput> orders)
        {
            this.orders = orders;
        }
    }
}