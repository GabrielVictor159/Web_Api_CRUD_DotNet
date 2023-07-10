using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Product;

namespace gcsb.ecommerce.application.UseCases.Product.UpdateProduct
{
    public interface IUpdateProductRequest
    {
         Task Execute(UpdateProductRequest request);
    }
}