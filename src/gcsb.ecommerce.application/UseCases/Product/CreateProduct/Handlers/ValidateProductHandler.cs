using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Product.CreateProduct.Handlers
{
    public class ValidateProductHandler : Handler<CreateProductRequest>
    {
        private readonly INotificationService _notificationService;
        public ValidateProductHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public override async Task ProcessRequest(CreateProductRequest request)
        {
           if(!request.Product.IsValid)
            {
                _notificationService.AddNotifications(request.Product.ValidationResult);
                return;
            }
            if(sucessor!=null)
            {
           await sucessor.ProcessRequest(request);
            }
        }
    }
}