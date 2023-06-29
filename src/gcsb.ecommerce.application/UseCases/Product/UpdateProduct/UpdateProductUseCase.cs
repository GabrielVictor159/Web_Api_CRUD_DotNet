using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.UseCases.Product.UpdateProduct.Handlers;

namespace gcsb.ecommerce.application.UseCases.Product.UpdateProduct
{
    public class UpdateProductUseCase : IUpdateProductRequest
    {
        private readonly IOutputPort<UpdateProductOutput> outputPort;
        private readonly ValidateProductHandler validateProductHandler;
        public UpdateProductUseCase(
            IOutputPort<UpdateProductOutput> outputPort,
            ValidateProductHandler validateProductHandler,
            UpdateProductHandler updateProductHandler)
        {
            this.outputPort=outputPort;
            this.validateProductHandler = validateProductHandler;
            this.validateProductHandler.SetSucessor(updateProductHandler);
        }
        public async Task Execute(UpdateProductRequest request)
        {
            try
            {
            await validateProductHandler.ProcessRequest(request);
            outputPort.Standard(
               request.productOutput!);
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}