using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Product.DeleteProduct.Handlers
{
    public class DeleteProductHandler : Handler<DeleteProductRequest>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public override async Task ProcessRequest(DeleteProductRequest request)
        {
            Boolean result = await productRepository.DeleteAsync(request.ProductId);
            if(result)
            {
                request.SetOutput(request.ProductId,"Product deleted successfully",true);
                if(sucessor!=null)
                {
                    await sucessor.ProcessRequest(request);
                }
                return;
            }
            request.SetOutput(request.ProductId,"Product not found");
        }
    }
}