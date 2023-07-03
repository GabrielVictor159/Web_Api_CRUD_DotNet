using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Client;
using gcsb.ecommerce.tests.Builder.Entities.Order;
using gcsb.ecommerce.tests.Builder.Entities.Product;
using gcsb.ecommerce.webapi.UseCases.Order.UpdateOrder;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Order.UpdateOrder
{
    [UseAutofacTestFramework]
    [Collection("OrderController")]
    public class OrderControllerTest
    {
        private readonly OrderController OrderController;
        private readonly INotificationService notificationService;
        private readonly Faker faker;
        private readonly Context context;
        private infrastructure.Database.Entities.Order? order;
        private List<infrastructure.Database.Entities.Product> products = new();
        public OrderControllerTest(
            INotificationService notificationService,
            OrderController OrderController,
            IHttpContextMethods httpContextMethods,
            Faker faker,
            Context context
        )
        {
            this.OrderController = OrderController;
            var claims = new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,Policies.ADMIN.ToString())

            };
            httpContextMethods.SetHttpContextWithClaims(claims,this.OrderController);
            this.notificationService = notificationService;
            this.faker = faker;
            this.context = context;
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
        public async Task ShouldOrderOutputNotBeNullAndHasNotificationsBeFalseAfterUpdateOrder()
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
            var newClient = await ClientBuilder.New(faker,context).Build();
            var request = new UpdateOrderRequest()
            {
                Id = order!.Id,
                IdClient = newClient.Id,
                Cupons = faker.Random.Enum<Cupons>().ToString(),
                IdPayment = Guid.NewGuid(),
                listProducts = listProducts,
                OrderDate = faker.Date.Past(100, DateTime.Now)
            };
            var response = await OrderController.UpdateOrder(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic) as OrderResponse;
            objectResponse!.orderOutput.Should().NotBeNull();
            notificationService.HasNotifications.Should().BeFalse();
        }
    }
}