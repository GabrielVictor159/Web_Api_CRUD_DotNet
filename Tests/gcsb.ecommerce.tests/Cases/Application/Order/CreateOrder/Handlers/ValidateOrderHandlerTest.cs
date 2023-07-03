using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
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
        private Faker faker;
        public ValidateOrderHandlerTest(
         ValidateOrderHandler validateOrderHandler,
         INotificationService notificationService,
         Faker faker)
        {
            this.validateOrderHandler = validateOrderHandler;
            this.notificationService = notificationService;
            this.faker = faker;
        }
        [Fact]
        public async Task ShouldHaveNotificationWhenOrderDomainIsInvalid()
        {
            var request = new CreateOrderRequest(Guid.NewGuid(),new List<application.Boundaries.Order.listProducts>());
            request.SetOrder(Builder.Domain.Order.OrderBuilder.New(faker).WithNameCupom("a").Build());
            await validateOrderHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldNotHavenNotificationWhenOrderDomainIsValid()
        {
            var request = new CreateOrderRequest(Guid.NewGuid(),new List<application.Boundaries.Order.listProducts>());
            request.SetOrder(Builder.Domain.Order.OrderBuilder.New(faker).Build());
            await validateOrderHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeFalse();
        }
    }
}