using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Product.CreateProduct
{
    public class CreateProductRequest
    {
        public required string Name {get;set;}
        public required decimal Value {get;set;}

    }
}