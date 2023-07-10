using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Product.CreateProduct
{
    public interface ICreateProductRequest
    {
        Task Execute(CreateProductRequest request);
    }
}