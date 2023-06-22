using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Order.RemoveOrder
{
    public class RemoveOrderRequest
    {
        public Guid Id {get; private set;}
        public string? orderResult {get; private set;}
        public RemoveOrderRequest(Guid id)
        {
            Id = id;
        }
        public void SetOutput(string orderResult)
        =>this.orderResult = orderResult;
    }
}