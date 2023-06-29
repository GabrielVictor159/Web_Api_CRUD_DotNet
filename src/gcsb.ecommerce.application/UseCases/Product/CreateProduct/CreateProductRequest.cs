using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.application.UseCases.Product.CreateProduct
{
    public class CreateProductRequest
    {
        public domain.Product.Product Product {get;private set;}
        public CreateProductOutput? productOutput {get;private set;}

        public CreateProductRequest(domain.Product.Product product)
        {
            this.Product = product;
        }
        public void SetOutput(Guid id, String Name, decimal Value)
        =>productOutput = new CreateProductOutput(id, Name, Value);

    }
}