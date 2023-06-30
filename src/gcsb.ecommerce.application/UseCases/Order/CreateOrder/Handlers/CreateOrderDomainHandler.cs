using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers
{
    public class CreateOrderDomainHandler : Handler<CreateOrderRequest>
    {
        private readonly IProductRepository productRepository;
        public CreateOrderDomainHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public override async Task ProcessRequest(CreateOrderRequest request)
        {
            Guid idOrder = Guid.NewGuid();
           List<domain.OrderProduct.OrderProduct> orderProducts = new();
           foreach(var p in request.listProducts)
           {
            var product = await productRepository.GetProductByIdAsync(p.Id);
            orderProducts.Add(new domain.OrderProduct.OrderProduct(p.Quantity,idOrder,product!));
           }
           request.SetOrder(new domain.Order.Order(idOrder,request.IdUser,orderProducts,DateTime.UtcNow,request.Cupons));
           if(sucessor!=null)
           {
            await sucessor.ProcessRequest(request);
           }
        }
    }
}