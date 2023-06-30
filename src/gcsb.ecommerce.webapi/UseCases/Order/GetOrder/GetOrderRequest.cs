using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Order.GetOrder
{
    public class GetOrderRequest
    {
        public string Id { get; set; } = "";
        public string IdClient { get; set; } = "";
        public string Cupons { get; set; } = "";
        public string IdPayment { get; set; } = "";
        public string OrderDate { get; set; } = "";
        public decimal MaximalOrder { get; set; } = decimal.MaxValue;
        public decimal MinimalOrder { get; set; } = decimal.MinValue;
        private int _pageSize = 10;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value!, 0, 100);
        }

        private int _page = 1;
        public int page
        {
            get => _page;
            set => _page = Math.Max(value!, 1);
        }

    }
}