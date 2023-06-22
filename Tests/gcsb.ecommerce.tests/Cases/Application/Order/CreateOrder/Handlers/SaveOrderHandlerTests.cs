using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.CreateOrder.Handlers
{
    [UseAutofacTestFramework]
    public class SaveOrderHandlerTests
    {
        private readonly SaveOrderHandler saveOrderHandler;
        private readonly Context context;
        public SaveOrderHandlerTests(SaveOrderHandler saveOrderHandler, Context context)
        {
            this.saveOrderHandler =saveOrderHandler;
            this.context = context; 
        }
        // [Fact]
        // public async void ShouldSaveOrderHandlerDomainValid()
        // {
        //     var request = new CreateOrderRequest(OrderBuilder.New().Build());
        //     await saveOrderHandler.ProcessRequest(request);
        //     var result = context.Orders.Find(request.Order.Id);
        //     result.Should().NotBeNull();
        // }
    }
}