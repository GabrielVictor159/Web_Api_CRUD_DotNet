using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Order
{
    public class CreateOrderOutput
    {
        public Guid Id { get; set; }
        public Guid IdClient { get; set; }
        public decimal TotalOrder { get; set; }
        public string Cupons { get; set; } = "";
        public List<OrderProductOutput> ListOrderProduct { get; set; } = new();
        public DateTime OrderDate { get; set; }
    }
}