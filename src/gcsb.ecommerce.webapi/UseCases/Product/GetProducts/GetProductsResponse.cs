using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.webapi.UseCases.Product.GetProducts
{
    public class GetProductsResponse
    {
        public List<ProductOutput> Products {get;set;}
        public GetProductsResponse(List<ProductOutput> Products)
        {
            this.Products = Products;
        }
    }
}