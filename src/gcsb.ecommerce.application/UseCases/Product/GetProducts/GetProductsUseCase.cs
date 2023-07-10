using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.UseCases.Product.GetProducts.Handlers;

namespace gcsb.ecommerce.application.UseCases.Product.GetProducts
{
    public class GetProductsUseCase : IGetProductsRequest
    {
        private readonly IOutputPort<List<ProductOutput>> outputPort;
        private readonly GetProductsHandler getProductsHandler;
        public GetProductsUseCase(IOutputPort<List<ProductOutput>> outputPort, GetProductsHandler getProductsHandler)
        {
            this.outputPort = outputPort;
            this.getProductsHandler = getProductsHandler;
        }
        public async Task Execute(GetProductsRequest request)
        {
            try
            {
            await getProductsHandler.ProcessRequest(request);
            outputPort.Standard(
               request.productsResult!);
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}