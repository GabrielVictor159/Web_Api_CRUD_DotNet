using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.RemoveOrder;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Entities.Order;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Order.RemoveOrder
{
    [UseAutofacTestFramework]
    public class RemoveOrderUseCaseTest
    {
        private readonly RemoveOrderUseCase RemoveOrderUseCase;
        private readonly INotificationService notificationService;
        private infrastructure.Database.Entities.Order? order;
        public RemoveOrderUseCaseTest(
            INotificationService notificationService,
            RemoveOrderUseCase RemoveOrderUseCase,
            Faker faker,
            Context context
        )
        {
            this.RemoveOrderUseCase = RemoveOrderUseCase;
            this.notificationService = notificationService;
            InitializeMethodsAsync(faker,context).Wait();
        }
         private async Task InitializeMethodsAsync(Faker faker,Context context)
        {
                OrderBuilder orderBuilder = await OrderBuilder.New(faker, context);
                 order = await orderBuilder.Build();
        }
        [Fact]
        public async Task ShouldHasNotificationsBeTrueAndOrderResultSucessBeFalseAfterProcessRequestBecauseInvalidId()
        {
            var request = new RemoveOrderRequest(Guid.NewGuid());
            await RemoveOrderUseCase.Execute(request);
            notificationService.HasNotifications.Should().BeTrue();
            request.orderResult!.Sucess.Should().BeFalse();
        }
        [Fact]
        public async Task ShouldHasNotificationsBeFalseAndOrderResultSucessBeTrueAfterProcessRequest()
        {
            var request = new RemoveOrderRequest(order!.Id);
            await RemoveOrderUseCase.Execute(request);
            notificationService.HasNotifications.Should().BeFalse();
            request.orderResult!.Sucess.Should().BeTrue(); 
        }

    }
}