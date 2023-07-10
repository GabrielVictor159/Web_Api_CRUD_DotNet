using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.webapi.UseCases.Product.UpdateProducts
{
    public class UpdateProductResponse
    {
        public UpdateProductOutput Product {get;set;}
        public UpdateProductResponse(UpdateProductOutput product)
        {
            this.Product = product;
        }
    }
}