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
            bool deleteResult = await orderRepository.DeleteAsync(request.Id);
            if(deleteResult)
            {
                request.SetOutput(new Boundaries.Order.DeleteOrderOutput(request.Id,"Order removed.",true));
            }
            else
            {
                request.SetOutput(new Boundaries.Order.DeleteOrderOutput(request.Id,"Order Not Found."));
                notificationService.AddNotification("Invalid Id", "There is no order with this Past Id");
            }
            if(sucessor!=null)
            {
            await sucessor.ProcessRequest(request);
            }
        }
    }
}