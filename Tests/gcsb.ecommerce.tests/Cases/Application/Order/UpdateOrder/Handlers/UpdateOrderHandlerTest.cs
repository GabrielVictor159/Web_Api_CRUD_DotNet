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
    public class UpdateOrderHandlerTest
    {
          private readonly UpdateOrderHandler UpdateOrderHandler;
          private readonly CreateListOrderProductHandler CreateListOrderProductHandler;
        private readonly INotificationService notificationService;
        private readonly Faker faker;
        private infrastructure.Database.Entities.Order? order;
        private List<infrastructure.Database.Entities.Product> products = new();
        public UpdateOrderHandlerTest(
            INotificationService notificationService,
            UpdateOrderHandler UpdateOrderHandler,
            CreateListOrderProductHandler CreateListOrderProductHandler,
            Faker faker,
            Context context
        )
        {
            this.CreateListOrderProductHandler = CreateListOrderProductHandler;
            this.UpdateOrderHandler = UpdateOrderHandler;
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
        public async Task ShouldOrderResultShouldNotBeNullAfterProcessRequest()
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
            var request = new UpdateOrderRequest(){Id=order!.Id, listProducts=listProducts};
            await CreateListOrderProductHandler.ProcessRequest(request);
            await UpdateOrderHandler.ProcessRequest(request);
            request.orderResult.Should().NotBeNull();
        }
    }
}