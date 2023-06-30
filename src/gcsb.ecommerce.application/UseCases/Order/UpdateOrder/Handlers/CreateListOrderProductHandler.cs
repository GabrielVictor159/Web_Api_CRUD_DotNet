using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder.Handlers
{
    public class CreateListOrderProductHandler : Handler<UpdateOrderRequest>
    {
        private readonly IProductRepository productRepository;
        public CreateListOrderProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public override async Task ProcessRequest(UpdateOrderRequest request)
        {
            if(request.listProducts!=null)
            {
           List<domain.OrderProduct.OrderProduct> orderProducts = new();
           foreach(var p in request.listProducts)
           {
            var product = await productRepository.GetProductByIdAsync(p.Id);
            orderProducts.Add(new domain.OrderProduct.OrderProduct(p.Quantity,request.Order.Id,product!));
           }
           request.Order.WithList(orderProducts);
            }
           if(sucessor!=null)
           {
            await sucessor.ProcessRequest(request);
           }
        }
     
    }
}