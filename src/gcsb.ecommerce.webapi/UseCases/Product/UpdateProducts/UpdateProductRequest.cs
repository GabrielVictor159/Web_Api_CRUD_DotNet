using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Product.UpdateProducts
{
    public class UpdateProductRequest
    {
        public required Guid Id {get;set;}
        public string? Name {get;set;}
        public decimal? Value {get;set;}
    }
}