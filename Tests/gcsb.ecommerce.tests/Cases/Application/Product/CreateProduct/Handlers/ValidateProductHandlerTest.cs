using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Product.CreateProduct;
using gcsb.ecommerce.application.UseCases.Product.CreateProduct.Handlers;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.CreateProduct.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateProductHandlerTest
    {
        private readonly Faker faker;
        private readonly ValidateProductHandler ValidateProductHandler;
        private readonly INotificationService notificationService;
        public ValidateProductHandlerTest(
            Faker faker, 
            ValidateProductHandler ValidateProductHandler,
            INotificationService notificationService)
        {
            this.faker = faker;
            this.ValidateProductHandler = ValidateProductHandler;
            this.notificationService = notificationService;
        }

        [Fact]
        public async Task ShouldBeTrueHasNotificationsByValidateProductHandler()
        {
            var request = new CreateProductRequest(ProductBuilder.New(faker).WithId(Guid.Empty).Build());
            await ValidateProductHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
    }
}