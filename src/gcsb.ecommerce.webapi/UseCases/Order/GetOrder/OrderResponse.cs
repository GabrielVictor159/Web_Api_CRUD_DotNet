using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Order.GetOrder
{
    public class OrderResponse
    {
        public List<domain.Order.Order> orders {get;private set;}
        public OrderResponse(List<domain.Order.Order> orders)
        {
            this.orders = orders;
        }
    }
}