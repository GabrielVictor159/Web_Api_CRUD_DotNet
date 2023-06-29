using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Product.GetProducts.Handlers
{
    public class GetProductsHandler : Handler<GetProductsRequest>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public override async Task ProcessRequest(GetProductsRequest request)
        {
           var result = await productRepository.GetProductAsync(request.func,request.page,request.pageSize);
            request.SetOutput(mapper.Map<List<ProductOutput>>(result));
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}