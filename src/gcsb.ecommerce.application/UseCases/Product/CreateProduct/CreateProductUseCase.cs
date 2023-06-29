using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.UseCases.Product.CreateProduct.Handlers;

namespace gcsb.ecommerce.application.UseCases.Product.CreateProduct
{
    public class CreateProductUseCase : ICreateProductRequest
    {
        private readonly IOutputPort<CreateProductOutput> outputPort;
        private readonly ValidateProductHandler validateProductHandler;
        public CreateProductUseCase(
            IOutputPort<CreateProductOutput> outputPort,
            ValidateProductHandler validateProductHandler,
            SaveProductHandler saveProductHandler)
        {
            this.outputPort = outputPort;
            this.validateProductHandler = validateProductHandler;
            this.validateProductHandler.SetSucessor(saveProductHandler);
        }
        public async Task Execute(CreateProductRequest request)
        {
            try
            {
            await validateProductHandler.ProcessRequest(request);
            outputPort.Standard(request.productOutput!);
            }
            catch (Exception ex)
            {
                outputPort.Error(ex.Message);
            }
        }
    }
}