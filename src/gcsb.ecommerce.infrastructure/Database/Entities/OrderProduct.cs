using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.infrastructure.Database.Entities
{
    public class OrderProduct
    {
        public int Amount { get; set; }
        public decimal TotalOrderLine { get; set; } = 0;
        public Guid IdOrder { get; set; }
        public Guid IdProduct { get; set; }
        public required Order Order { get; set; }
        public required Product Product { get; set; }
    }
}
