using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Order.UpdateOrder.Handlers
{
    public class ValidateOrderHandler : Handler<UpdateOrderRequest>
    {
        private readonly INotificationService _notificationService;
        private readonly IOrderRepository _orderRepository;
        private readonly IReflectionMethods _reflectionMethods;
        public ValidateOrderHandler(INotificationService notificationService, IOrderRepository orderRepository, IReflectionMethods reflectionMethods)
        {
            _notificationService = notificationService;
            _orderRepository = orderRepository;
            _reflectionMethods = reflectionMethods;
        }
        public override async Task ProcessRequest(UpdateOrderRequest request)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Order.Id);
            if(order == null)
            {
                _notificationService.AddNotification("Invalid Order Id", $"Could not find an order with that id: {request.Order.Id}");
                return;
            }
            _reflectionMethods.ReplaceDifferentAttributes(request.Order,order);
            if(!order.IsValid)
            {
                _notificationService.AddNotifications(order.ValidationResult);
                return;
            }
            request.NewAttributesOrder = order;
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}