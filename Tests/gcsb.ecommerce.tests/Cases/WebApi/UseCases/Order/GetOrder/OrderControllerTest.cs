using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Order;
using gcsb.ecommerce.webapi.UseCases.Order.GetOrder;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Order.GetOrder
{
    [UseAutofacTestFramework]
    [Collection("OrderController")]
    public class OrderControllerTest
    {
        private readonly OrderController OrderController;
       private List<infrastructure.Database.Entities.Order> orders = new();
       public OrderControllerTest(
       OrderController OrderController,
       Faker faker,
       Context context,
       IHttpContextMethods httpContextMethods
       )
       {
        this.OrderController = OrderController;
        var claims = new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,Policies.ADMIN.ToString())

            };
        httpContextMethods.SetHttpContextWithClaims(claims,this.OrderController);
        InitializeMethodsAsync(faker,context).Wait();
       }  
        private async Task InitializeMethodsAsync(Faker faker,Context context)
        {
            for (int i = 0; i < faker.Random.Int(2, 5); i++)
            {
               OrderBuilder orderBuilder = await OrderBuilder.New(faker, context);
                infrastructure.Database.Entities.Order order = await orderBuilder.Build();
                orders.Add(order);
            }
        }

        [Fact]
        public async Task ShouldOrderResultNotBeNullOrEmptyAfterGetOrder()
        {
            var request = new GetOrderRequest();
            var response = await OrderController.GetOrder(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic) as OrderResponse;
            objectResponse!.orders.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task ShouldOrderResultCountBe1AfterGetOrder()
        {
             var request = new GetOrderRequest(){Id=orders[0].Id.ToString()};
            var response = await OrderController.GetOrder(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic) as OrderResponse;
            objectResponse!.orders.Count.Should().Be(1);
        }
    }
}