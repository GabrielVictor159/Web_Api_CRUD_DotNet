using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Client;
using gcsb.ecommerce.tests.Builder.Entities.OrderProduct;
using gcsb.ecommerce.tests.Builder.Entities.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.CreateOrder.Handlers
{
    [UseAutofacTestFramework]
    public class SaveOrderHandlerTest
    {
        private readonly SaveOrderHandler SaveOrderHandler;
        private readonly CreateOrderDomainHandler createOrderDomainHandler;
        private readonly INotificationService notificationService;
        private readonly List<infrastructure.Database.Entities.Product> products = new();
        private readonly Faker faker;
        private readonly Context context;
        private infrastructure.Database.Entities.Client? client;

        public SaveOrderHandlerTest(
            SaveOrderHandler SaveOrderHandler,
            CreateOrderDomainHandler CreateOrderDomainHandler,
            INotificationService notificationService,
            Faker faker,
            Context context)
        {
            this.SaveOrderHandler = SaveOrderHandler;
            this.notificationService = notificationService;
            this.faker = faker;
            this.context = context;
            this.createOrderDomainHandler = CreateOrderDomainHandler;
            InitializeMethodsAsync(context).Wait();
        }

        private async Task InitializeMethodsAsync(Context context)
        {
            for (int i = 0; i < faker.Random.Int(2, 10); i++)
            {
                var product = await ProductBuilder.New(faker, context).Build();
                products.Add(product);
            }

            client = await ClientBuilder.New(faker, context).Build();
        }

        [Fact]
        public async Task ShouldOrderOutputNotBeNullAndOrderPesistNotBeNullAfterProcessRequest()
        {
            List<application.Boundaries.Order.listProducts> listProducts = new();
            foreach(var product in products)
            {
                listProducts.Add(new application.Boundaries.Order.listProducts(){Id=product.Id,Quantity=faker.Random.Int(2, 10)});
            }
            var request = new CreateOrderRequest(client!.Id,listProducts);
            await createOrderDomainHandler.ProcessRequest(request);
            await SaveOrderHandler.ProcessRequest(request);
            request.OrderOutput.Should().NotBeNull();
            var orderPersist = await context.Orders.FindAsync(request.OrderOutput!.Id);
            orderPersist.Should().NotBeNull();
        }
    }
}
