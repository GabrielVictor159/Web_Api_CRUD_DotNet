using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.UpdateOrder;
using gcsb.ecommerce.application.UseCases.Order.UpdateOrder.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Order;
using gcsb.ecommerce.tests.Builder.Entities.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.UpdateOrder.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateOrderHandlerTest
    {
        private readonly ValidateOrderHandler ValidateOrderHandler;
        private readonly INotificationService notificationService;
        private readonly Faker faker;
        private infrastructure.Database.Entities.Order? order;
        private List<infrastructure.Database.Entities.Product> products = new();
        public ValidateOrderHandlerTest(
            INotificationService notificationService,
            ValidateOrderHandler ValidateOrderHandler,
            Faker faker,
            Context context
        )
        {
            this.ValidateOrderHandler = ValidateOrderHandler;
            this.notificationService = notificationService;
            this.faker = faker;
            InitializeMethodsAsync(faker,context).Wait();
        }
         private async Task InitializeMethodsAsync(Faker faker,Context context)
        {
                for(int i = 0; i < faker.Random.Int(2,10);i++)
                {
                    products.Add(await ProductBuilder.New(faker,context).Build());
                }
                OrderBuilder orderBuilder = await OrderBuilder.New(faker, context);
                 order = await orderBuilder.Build();
        }
        [Fact]
        public async Task ShouldHasNotifiactionsBeFalseAfterProcessRequest()
        {
             var request = new UpdateOrderRequest(){
                Id=order!.Id,
                NewAttributesOrder = Builder.Domain.Order.OrderBuilder.New(faker).WithId(order.Id).Build()
                };
             await ValidateOrderHandler.ProcessRequest(request);
             notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldHasNotifiactionsBeTrueAfterProcessRequestInvalidCupom()
        {
             var request = new UpdateOrderRequest(){
                Id=order!.Id, 
                Cupons="ad",
                NewAttributesOrder = 
                Builder.Domain.Order.OrderBuilder.New(faker).WithId(order.Id).WithNameCupom("ad").Build()};
             await ValidateOrderHandler.ProcessRequest(request);
             notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldHasNotifiactionsBeTrueAfterProcessRequestInvalidId()
        {
             var request = new UpdateOrderRequest(){
                Id=Guid.NewGuid(),
                NewAttributesOrder = 
                Builder.Domain.Order.OrderBuilder.New(faker).WithId(Guid.NewGuid()).Build()};
             await ValidateOrderHandler.ProcessRequest(request);
             notificationService.HasNotifications.Should().BeTrue();
        }
    }
}