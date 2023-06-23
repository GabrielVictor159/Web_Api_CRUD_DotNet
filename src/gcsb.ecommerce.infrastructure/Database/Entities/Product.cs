using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.infrastructure.Database.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Value { get; set; }
        public List<OrderProduct> ListOrderProduct { get; set; } = new List<OrderProduct>();
    }
}
