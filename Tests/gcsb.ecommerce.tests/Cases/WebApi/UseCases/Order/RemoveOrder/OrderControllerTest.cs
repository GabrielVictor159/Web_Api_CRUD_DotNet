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
using gcsb.ecommerce.tests.Builder.Entities.Order;
using gcsb.ecommerce.webapi.UseCases.Order.RemoveOrder;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Order.RemoveOrder
{
    [UseAutofacTestFramework]
    [Collection("OrderController")]
    public class OrderControllerTest
    {
         private readonly OrderController OrderController;
        private readonly INotificationService notificationService;
        private infrastructure.Database.Entities.Order? order;
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
            InitializeMethodsAsync(faker,context).Wait();
        }
         private async Task InitializeMethodsAsync(Faker faker,Context context)
        {
                OrderBuilder orderBuilder = await OrderBuilder.New(faker, context);
                order = await orderBuilder.Build();
        }
        [Fact]
        public async Task ShouldDeleteRequestSucessBeFalseRemoveOrderInvalidId()
        {
            var request = new RemoveOrderRequest(){id=Guid.NewGuid()};
            var response = await OrderController.RemoveOrder(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic) as OrderResponse;
            objectResponse!.deleteRequest.Sucess.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldDeleteRequestSucessBeTrueRemoveOrder()
        {
            var request = new RemoveOrderRequest(){id=order!.Id};
            var response = await OrderController.RemoveOrder(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic) as OrderResponse;
            objectResponse!.deleteRequest.Sucess.Should().BeTrue();
        }
    }
}