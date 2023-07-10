using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder
{
    public class UpdateOrderRequest
    {
         public required  Guid Id { get;  set; }
        public Guid? IdClient { get;  set; }
        public string? Cupons { get;  set; }
        public Guid? IdPayment { get; set; }
        public List<listProducts>? listProducts {get;set;}
        public DateTime? OrderDate { get;  set; }
        public domain.Order.Order? NewAttributesOrder {get;set;}
        public UpdateOrderOutput? orderResult {get; private set; }

        public void SetOutput(UpdateOrderOutput output)
        => orderResult = output;
    }
}