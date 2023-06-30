using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.Boundaries.Order
{
    public class OrderOutput
    {
        public Guid Id { get;  set; }
        public Guid IdClient { get; set; }
        public decimal TotalOrder { get;  set; }
        public string Cupons { get;  set; } = "";
        public Guid IdPayment { get; set; }
        public List<domain.OrderProduct.OrderProduct> ListOrderProduct { get;  set; } = new();
        public DateTime OrderDate { get;  set; }
    }
}