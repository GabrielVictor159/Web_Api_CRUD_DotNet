using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public domain.Order.Order Order { get; private set; }
        public domain.Order.Order? orderResult {get; private set; }
        public UpdateOrderRequest(domain.Order.Order order)
        {
            Order = order;
        }
        public void SetOutput(domain.Order.Order output)
        => orderResult = output;
    }
}