using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.infrastructure.Database.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public List<Order> ListOrder { get; set; } = new List<Order>();
    }
}
