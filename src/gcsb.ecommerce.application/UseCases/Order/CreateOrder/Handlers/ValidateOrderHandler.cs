using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers
{
    public class ValidateOrderHandler : Handler<CreateOrderRequest>
    {
        private readonly INotificationService _notificationService;
        public ValidateOrderHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public override async Task ProcessRequest(CreateOrderRequest request)
        {
            if(!request.Order!.IsValid)
            {
                _notificationService.AddNotifications(request.Order.ValidationResult);
                return;
            }
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}