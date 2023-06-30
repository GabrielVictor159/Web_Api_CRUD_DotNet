using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Order
{
    public class OrderProductOutput
    {
        public int Amount { get; set; }
        public decimal TotalOrderLine { get; set; } = 0;
        public Guid IdProduct { get; set; }
    }
}