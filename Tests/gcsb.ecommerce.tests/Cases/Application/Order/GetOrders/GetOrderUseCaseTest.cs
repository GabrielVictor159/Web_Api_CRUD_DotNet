using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.UseCases.Order.GetOrder;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Order;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.GetOrders
{
    [UseAutofacTestFramework]
    public class GetOrderUseCaseTest
    {
        private readonly GetOrderUseCase GetOrderUseCase;
       private List<infrastructure.Database.Entities.Order> orders = new();
       public GetOrderUseCaseTest(
       GetOrderUseCase GetOrderUseCase,
       Faker faker,
       Context context
       )
       {
        this.GetOrderUseCase = GetOrderUseCase;
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
        public async Task ShouldOrderResultNotBeNullOrEmptyAfterExecute()
        {
            Expression<Func<domain.Order.Order, bool>> func = e=> true;
            var request = new GetOrderRequest(func,1,10);
            await GetOrderUseCase.Execute(request);
            request.orderResult.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task ShouldOrderResultCountBe1AfterExecute()
        {
            Expression<Func<domain.Order.Order, bool>> func = e=> e.Id==orders[0].Id;
            var request = new GetOrderRequest(func,1,10);
            await GetOrderUseCase.Execute(request);
            request.orderResult!.Count.Should().Be(1);
        }
    }
}