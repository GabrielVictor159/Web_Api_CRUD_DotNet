using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Product.DeleteProducts
{
    public class DeleteProductRequest
    {
        public required Guid ProductId {get;set;} 
    }
}