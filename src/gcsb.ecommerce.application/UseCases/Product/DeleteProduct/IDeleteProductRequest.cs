using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Product.DeleteProduct
{
    public interface IDeleteProductRequest
    {
        Task Execute(DeleteProductRequest request);
    }
}