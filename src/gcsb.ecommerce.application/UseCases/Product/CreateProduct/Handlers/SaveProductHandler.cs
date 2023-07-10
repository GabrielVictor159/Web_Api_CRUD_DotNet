using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Product.CreateProduct.Handlers
{
    public class SaveProductHandler : Handler<CreateProductRequest>
    {
        public readonly IProductRepository productRepository;
        public SaveProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public override async Task ProcessRequest(CreateProductRequest request)
        {
            var result = await productRepository.CreateAsync(request.Product);
            request.SetOutput(result.Id,result.Name,result.Value);
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}