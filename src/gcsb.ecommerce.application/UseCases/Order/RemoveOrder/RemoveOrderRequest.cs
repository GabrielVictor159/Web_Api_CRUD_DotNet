using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.application.UseCases.Order.RemoveOrder
{
    public class RemoveOrderRequest
    {
        public Guid Id {get; private set;}
        public DeleteOrderOutput? orderResult {get; private set;}
        public RemoveOrderRequest(Guid id)
        {
            Id = id;
        }
        public void SetOutput(DeleteOrderOutput orderResult)
        =>this.orderResult = orderResult;
    }
}