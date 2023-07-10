using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.application.UseCases.Product.UpdateProduct
{
    public class UpdateProductRequest
    {
        public domain.Product.Product Product { get; set; }
        public UpdateProductOutput? productOutput {get;set;}
        public  UpdateProductRequest(domain.Product.Product product)
        {
            Product = product;
        }
        public void SetOutput(UpdateProductOutput output)
        => this.productOutput = output;
    }
}