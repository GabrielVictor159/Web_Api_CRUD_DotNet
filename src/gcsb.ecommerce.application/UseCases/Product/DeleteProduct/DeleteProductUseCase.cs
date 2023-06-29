using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.UseCases.Product.DeleteProduct.Handlers;

namespace gcsb.ecommerce.application.UseCases.Product.DeleteProduct
{
    public class DeleteProductUseCase : IDeleteProductRequest
    {
        private readonly IOutputPort<DeleteProductOutput> outputPort;
        private readonly DeleteProductHandler deleteProductHandler;
        public DeleteProductUseCase(
            IOutputPort<DeleteProductOutput> outputPort, 
            DeleteProductHandler deleteProductHandler)
        {
            this.outputPort = outputPort;
            this.deleteProductHandler = deleteProductHandler;
        }
        public async Task Execute(DeleteProductRequest request)
        {
             try
            {
            await deleteProductHandler.ProcessRequest(request);
            outputPort.Standard(
               request.deleteOutput!);
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}