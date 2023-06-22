using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.CreateOrder
{
    [UseAutofacTestFramework]
    public class CreateOrderUseCaseTest
    {
        public readonly ICreateOrderRequest createOrderUseCase;
        public CreateOrderUseCaseTest(
            ICreateOrderRequest createOrderUseCase)
        {
            this.createOrderUseCase = createOrderUseCase;
        } 
        // [Fact]
        // public async Task ShouldCreateOrderUseCaseExecuteDomainValid()
        // {
        //     var request = new CreateOrderRequest(OrderBuilder.New().Build());
        //     await createOrderUseCase.Execute(request);
        //     request.OrderOutput.Should().NotBeNull();
        // }
    }
}