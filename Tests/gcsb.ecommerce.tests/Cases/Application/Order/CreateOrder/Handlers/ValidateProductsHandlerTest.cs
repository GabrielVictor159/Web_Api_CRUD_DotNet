using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.CreateOrder.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateProductsHandlerTest
    {
         private readonly ValidateProductsHandler validateProductsHandler;
        private readonly INotificationService notificationService;
        private readonly List<infrastructure.Database.Entities.Product> products = new();
        private readonly Faker faker;
        public ValidateProductsHandlerTest(
            ValidateProductsHandler validateProductsHandler,
            INotificationService notificationService,
            Faker faker,
            Context context)
        {
            this.validateProductsHandler = validateProductsHandler;
            this.notificationService = notificationService;
            this.faker = faker;
            InitializeMethodsAsync(context).Wait();
        }

        private async Task InitializeMethodsAsync(Context context)
        {
            for (int i = 0; i < faker.Random.Int(2, 10); i++)
            {
                var product = await ProductBuilder.New(faker, context).Build();
                products.Add(product);
            }
        }
        [Fact]
        public async Task ShouldHasNotificationBeFalseByProcessRequest()
        {
            List<listProducts> listProducts = new();
            foreach(var product in products)
            {
                listProducts.Add(new listProducts(){Id=product.Id,Quantity=faker.Random.Int(2, 10)});
            }
            var request = new CreateOrderRequest(Guid.NewGuid(),listProducts);
            await validateProductsHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldHasNotificationBeTrueByProcessRequestProductInexist()
        {
            List<listProducts> listProducts = new();
            listProducts.Add(new listProducts(){Id=Guid.NewGuid(),Quantity=faker.Random.Int(2, 10)});
            var request = new CreateOrderRequest(Guid.NewGuid(),listProducts);
            await validateProductsHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldHasNotificationBeTrueByProcessRequestRepeteProduct()
        {
            List<listProducts> listProducts = new();
            listProducts.Add(new listProducts(){Id=products[0].Id,Quantity=faker.Random.Int(2, 10)});
            listProducts.Add(new listProducts(){Id=products[0].Id,Quantity=faker.Random.Int(2, 10)});
            var request = new CreateOrderRequest(Guid.NewGuid(),listProducts);
            await validateProductsHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}