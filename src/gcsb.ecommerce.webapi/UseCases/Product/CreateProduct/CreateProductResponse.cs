using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.webapi.UseCases.Product.CreateProduct
{
    public class CreateProductResponse
    {
        public CreateProductOutput product {get; set;}
        public CreateProductResponse(CreateProductOutput output)
        {
            this.product = output;
        }
    }
}