using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.webapi.UseCases.Order.UpdateOrder
{
    public class UpdateOrderRequest
    {
        public required  Guid Id { get;  set; }
        public Guid? IdClient { get;  set; }
        public string? Cupons { get;  set; }
        public Guid? IdPayment { get; set; }
        public List<listProducts>? listProducts {get;set;}
        public DateTime? OrderDate { get;  set; }
    }
}