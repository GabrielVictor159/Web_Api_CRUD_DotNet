using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Product.UpdateProduct.Handlers
{
    public class UpdateProductHandler : Handler<UpdateProductRequest>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public override async Task ProcessRequest(UpdateProductRequest request)
        {
            var result = await productRepository.UpdateAsync(request.Product);
            request.SetOutput(mapper.Map<UpdateProductOutput>(result));
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}