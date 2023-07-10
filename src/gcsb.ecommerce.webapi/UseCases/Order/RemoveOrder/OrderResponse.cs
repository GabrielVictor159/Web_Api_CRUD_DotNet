using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.webapi.UseCases.Order.RemoveOrder
{
    public class OrderResponse
    {
        public DeleteOrderOutput deleteRequest {get;private set;}
        public OrderResponse(DeleteOrderOutput deleteRequest)
        {
            this.deleteRequest = deleteRequest;
        }
    }
}