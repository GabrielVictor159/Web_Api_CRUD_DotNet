using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gcsb.ecommerce.infrastructure.Database.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public required Client Client { get; set; }
        public Guid IdClient { get; set; }
        public decimal TotalOrder { get; set; }
        public string Cupons { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public Guid IdPayment { get; set; }
        public List<OrderProduct> ListOrderProduct { get; set; } = new List<OrderProduct>();
    }
}
