using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.webapi.UseCases.Product.DeleteProducts
{
    public class DeleteProductResponse
    {
        public DeleteProductOutput DeleteRequest {get;set;}
        public DeleteProductResponse(DeleteProductOutput deleteRequest)
        {
            this.DeleteRequest = deleteRequest;
        }
    }
}