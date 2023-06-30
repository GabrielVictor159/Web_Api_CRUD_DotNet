using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public domain.Order.Order Order { get; private set; }
        public domain.Order.Order? NewAttributesOrder {get;set;}
        public UpdateOrderOutput? orderResult {get; private set; }
        public List<(Guid Id, int Quantity)>? listProducts {get;set;}
        public UpdateOrderRequest(domain.Order.Order order)
        {
            Order = order;
        }
        public void SetOutput(UpdateOrderOutput output)
        => orderResult = output;
    }
}