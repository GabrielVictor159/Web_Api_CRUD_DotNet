using System;

namespace gcsb.ecommerce.webapi.UseCases.Product.GetProducts
{
    public class GetProductsRequest
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal MinValue { get; set; } = 0;
        public decimal MaxValue { get; set; } = decimal.MaxValue;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(value, 100); 
        }

        public int Page { get; set; } = 1;
    }
}
