using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gcsb.ecommerce.application.UseCases.Product.GetProducts
{
    public interface IGetProductsRequest
    {
        Task Execute(GetProductsRequest request);
    }
}