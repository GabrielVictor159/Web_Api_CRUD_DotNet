using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Product.UpdateProduct.Handlers
{
    public class ValidateProductHandler : Handler<UpdateProductRequest>
    {
        private readonly INotificationService notificationService;
        private readonly IProductRepository productRepository;
        private readonly IReflectionMethods reflectionMethods;
        public ValidateProductHandler(
            INotificationService notificationService, 
            IProductRepository productRepository,
            IReflectionMethods reflectionMethods)
        {
            this.notificationService = notificationService;
            this.productRepository = productRepository;
            this.reflectionMethods = reflectionMethods;
        }
        public override async Task ProcessRequest(UpdateProductRequest request)
        {
            var productList = await productRepository.GetProductAsync(e => e.Id == request.Product.Id);
            var product = productList.FirstOrDefault();
            if(product==null)
            {
                notificationService.AddNotification("Product not found",$"No product found with id {request.Product.Id}");
                return;
            }
            reflectionMethods.ReplaceDifferentAttributes(request.Product,product);
            product.ValidateEntity();
            if(!product.IsValid)
            {
                notificationService.AddNotifications(product.ValidationResult);
                return;
            }
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}