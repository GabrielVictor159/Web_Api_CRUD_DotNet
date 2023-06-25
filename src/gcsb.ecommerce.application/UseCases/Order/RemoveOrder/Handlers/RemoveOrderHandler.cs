using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;

namespace gcsb.ecommerce.application.UseCases.Order.RemoveOrder.Handlers
{
    public class RemoveOrderHandler : Handler<RemoveOrderRequest>
    {
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;
        public RemoveOrderHandler(
            IOrderRepository orderRepository,
            INotificationService notificationService)
        {
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
        }

        public override async Task ProcessRequest(RemoveOrderRequest request)
        {
            // bool deleteResult = await orderRepository.Delete(request.Id);
            // if(deleteResult)
            // {
            //     request.SetOutput($"Order with Id: {request.Id} succesfully deleted");
            // }
            // else
            // {
            //     notificationService.AddNotification("Invalid Id", "There is no order with this Past Id");
            // }
            sucessor?.ProcessRequest(request);
        }
    }
}