using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers;
using gcsb.ecommerce.tests.Builder.Domain;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.CreateOrder.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateOrderHandlerTest
    {
        private readonly ValidateOrderHandler validateOrderHandler;
        private readonly INotificationService notificationService;
        public ValidateOrderHandlerTest(
         ValidateOrderHandler validateOrderHandler,
         INotificationService notificationService)
        {
            this.validateOrderHandler = validateOrderHandler;
            this.notificationService = notificationService;
        }
        // [Fact]
        // public async Task ShouldHaveNotificationWhenDomainInvalid()
        // {
        //     var request = new CreateOrderRequest(OrderBuilder.New().WithTotalOrder(0).Build());
        //     await validateOrderHandler.ProcessRequest(request);
        //     notificationService.HasNotifications.Should().BeTrue();
        // }
    }
}