using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.RemoveOrder;
using gcsb.ecommerce.application.UseCases.Order.RemoveOrder.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Order;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.RemoveOrder.Handlers
{
    [UseAutofacTestFramework]
    public class RemoveOrderHandlerTest
    {
        private readonly RemoveOrderHandler removeOrderHandler;
        private readonly INotificationService notificationService;
        private infrastructure.Database.Entities.Order? order;
        public RemoveOrderHandlerTest(
            INotificationService notificationService,
            RemoveOrderHandler removeOrderHandler,
            Faker faker,
            Context context
        )
        {
            this.removeOrderHandler = removeOrderHandler;
            this.notificationService = notificationService;
            InitializeMethodsAsync(faker,context).Wait();
        }
         private async Task InitializeMethodsAsync(Faker faker,Context context)
        {
                OrderBuilder orderBuilder = await OrderBuilder.New(faker, context);
                 order = await orderBuilder.Build();
        }
        [Fact]
        public async Task ShouldHasNotificationsBeTrueAndOrderResultSucessBeFalseAfterProcessRequestBecauseInvalidId()
        {
            var request = new RemoveOrderRequest(Guid.NewGuid());
            await removeOrderHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
            request.orderResult!.Sucess.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldHasNotificationsBeFalseAndOrderResultSucessBeTrueAfterProcessRequest()
        {
            var request = new RemoveOrderRequest(order!.Id);
            await removeOrderHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeFalse();
            request.orderResult!.Sucess.Should().BeTrue(); 
        }

    }
    
}