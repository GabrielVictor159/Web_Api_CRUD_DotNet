using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Boundaries.Order;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Client;
using gcsb.ecommerce.tests.Builder.Entities.Product;
using gcsb.ecommerce.webapi.UseCases.Order.CreateOrder;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Order.CreateOrder
{
    [UseAutofacTestFramework]
    [Collection("OrderController")]
    public class OrderControllerTest
    {
        private readonly Faker faker;
        private readonly OrderController controller;
        private readonly INotificationService notificationService;
        private readonly IHttpContextMethods httpContextMethods;
        private infrastructure.Database.Entities.Client? client;
        private List<infrastructure.Database.Entities.Product> products = new();

        public OrderControllerTest(
            Faker faker,
            OrderController controller,
            INotificationService notificationService,
            IHttpContextMethods httpContextMethods,
            Context context)
        {
            this.faker = faker;
            this.controller = controller;
            this.notificationService = notificationService;
            this.httpContextMethods = httpContextMethods;
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
        public async Task ShouldObjectResponseNotBeNullAndHasNotificantionBeFalseAfterCreateOrder()
        {
            var claims = new[]
            {
                new Claim("Id", client!.Id.ToString()),
            };
            httpContextMethods.SetHttpContextWithClaims(claims, controller);
            List<application.Boundaries.Order.listProducts> listProducts = new();
            foreach (var product in products)
            {
                listProducts.Add(new application.Boundaries.Order.listProducts() { Id = product.Id, Quantity = faker.Random.Int(2, 10) });
            }
            var request = new CreateOrderRequest() { listProducts = listProducts };
            var response = await controller.CreateOrder(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic) as OrderResponse;
            objectResponse!.orderOutput.Should().NotBeNull();
            notificationService.HasNotifications.Should().BeFalse();
        }
    }
}
