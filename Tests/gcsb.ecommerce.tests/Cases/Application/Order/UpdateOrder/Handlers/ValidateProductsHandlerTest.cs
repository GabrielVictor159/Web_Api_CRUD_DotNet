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
    public class ValidateProductsHandlerTest
    {
         private readonly ValidateProductsHandler ValidateProductsHandler;
        private readonly INotificationService notificationService;
        private readonly Faker faker;
        private infrastructure.Database.Entities.Order? order;
        private List<infrastructure.Database.Entities.Product> products = new();
        public ValidateProductsHandlerTest(
            INotificationService notificationService,
            ValidateProductsHandler ValidateProductsHandler,
            Faker faker,
            Context context
        )
        {
            this.ValidateProductsHandler = ValidateProductsHandler;
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
        public async Task ShouldHasNotifiactionsBeFalseAfterProcessRequestListProductsEmpty()
        {
            var request = new UpdateOrderRequest(){Id=order!.Id};
            await ValidateProductsHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldHasNotifiactionsBeFalseAfterProcessRequestValidListProducts()
        {
            List<application.Boundaries.Order.listProducts> listProducts = new();
            foreach(var product in products)
            {
                listProducts.Add(
                    new application.Boundaries.Order.listProducts()
                    {
                        Id=product.Id, 
                        Quantity = faker.Random.Int(2,10),
                    }
                    );
            }
            var request = new UpdateOrderRequest(){Id=order!.Id};
            await ValidateProductsHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldHasNotifiactionsBeTrueAfterProcessRequestListProductsInvalidRepeatProduct()
        {
            List<application.Boundaries.Order.listProducts> listProducts = new();
                listProducts.Add(
                    new application.Boundaries.Order.listProducts()
                    {
                        Id=products[0].Id, 
                        Quantity = faker.Random.Int(2,10),
                    }
                    );
                listProducts.Add(
                    new application.Boundaries.Order.listProducts()
                    {
                        Id=products[0].Id, 
                        Quantity = faker.Random.Int(2,10),
                    }
                    );
            var request = new UpdateOrderRequest(){Id=order!.Id, listProducts =listProducts};
            await ValidateProductsHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldHasNotifiactionsBeTrueAfterProcessRequestListProductsInvalidProductInexist()
        {
            List<application.Boundaries.Order.listProducts> listProducts = new();
                listProducts.Add(
                    new application.Boundaries.Order.listProducts()
                    {
                        Id=Guid.NewGuid(), 
                        Quantity = faker.Random.Int(2,10),
                    }
                    );
            var request = new UpdateOrderRequest(){Id=order!.Id, listProducts =listProducts};
            await ValidateProductsHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}