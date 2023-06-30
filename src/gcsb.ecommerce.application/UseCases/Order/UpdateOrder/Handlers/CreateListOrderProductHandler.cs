using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder.Handlers
{
    public class CreateListOrderProductHandler : Handler<UpdateOrderRequest>
    {
        private readonly IProductRepository productRepository;
        private readonly IReflectionMethods reflectionMethods;
        public CreateListOrderProductHandler(IProductRepository productRepository, IReflectionMethods reflectionMethods)
        {
            this.productRepository = productRepository;
            this.reflectionMethods = reflectionMethods;
        }
        public override async Task ProcessRequest(UpdateOrderRequest request)
        {
            List<domain.OrderProduct.OrderProduct> orderProducts = new();
            if(request.listProducts!=null)
            {
           foreach(var p in request.listProducts)
           {
            var product = await productRepository.GetProductByIdAsync(p.Id);
            orderProducts.Add(new domain.OrderProduct.OrderProduct(p.Quantity,request.Id,product!));
           }
            }
             domain.Order.Order newOrder = new domain.Order.Order();
            reflectionMethods.ReplaceDifferentAttributes(request,newOrder);
            newOrder.WithList(orderProducts);
            newOrder.ValidateEntity();
            request.NewAttributesOrder = newOrder;
           if(sucessor!=null)
           {
            await sucessor.ProcessRequest(request);
           }
        }
     
    }
}