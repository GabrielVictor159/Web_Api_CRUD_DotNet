using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder.Handlers
{
    public class ValidateProductsHandler : Handler<UpdateOrderRequest>
    {
        private readonly IProductRepository productRepository;
        private readonly INotificationService notificationService;
        public ValidateProductsHandler(IProductRepository productRepository, INotificationService notificationService)
        {
            this.productRepository = productRepository;
            this.notificationService = notificationService;
        }
        public override async Task ProcessRequest(UpdateOrderRequest request)
        {
            if(request.listProducts!=null)
            {
            bool repeatItems = false;
            bool inexistItems = false;
            foreach(var p in request.listProducts)
            {
               var items = request.listProducts.Where(a => a.Id==p.Id).ToList();
               if(items.Count>1)
               {
                notificationService.AddNotification("invalid product list",$"The product with the Id {p.Id} is being repeated in the list");
                repeatItems = true;
               }
               var searchItem = await productRepository.GetProductByIdAsync(p.Id);
               if(searchItem==null)
               {
                notificationService.AddNotification("invalid product list",$"Product with Id {p.Id} does not exist.");
                inexistItems = true;
               }
            }
            if(!repeatItems && !inexistItems && sucessor!=null)
               {
                await sucessor.ProcessRequest(request);
               }
            }
            else
            {
                if(sucessor!=null)
                {
                    await sucessor.ProcessRequest(request);
                }
            }
        }
    }
}