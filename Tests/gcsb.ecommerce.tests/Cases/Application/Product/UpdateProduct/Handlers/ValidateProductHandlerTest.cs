using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Product.UpdateProduct;
using gcsb.ecommerce.application.UseCases.Product.UpdateProduct.Handlers;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.UpdateProduct.Handlers
{
    [UseAutofacTestFramework]
    public class ValidateProductHandlerTest
    {
        private readonly Faker faker;
        private readonly INotificationService notificationService;
        private readonly ValidateProductHandler validateProductHandler;
        private domain.Product.Product product;
        public ValidateProductHandlerTest(
            Faker faker,
            INotificationService notificationService,
            IProductRepository productRepository,
            ValidateProductHandler validateProductHandler
        )
        {
            this.faker = faker;
            this.notificationService = notificationService;
            this.validateProductHandler = validateProductHandler;
            product = ProductBuilder.New(faker).Build();
            productRepository.CreateAsync(product).Wait();
        }
        [Fact]
        public async Task ShouldHasNotificationBeTrueByValidateProductHandlerInvalidProduct()
        {
            var request = new UpdateProductRequest(ProductBuilder.New(faker).Build());
            await validateProductHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldHasNotificationBeTrueByValidateProductHandlerInvalidDomain()
        {
            var request = new UpdateProductRequest(ProductBuilder.New(faker).WithId(product.Id).WithName("ad").Build());
            await validateProductHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldHasNotificationBeFalseByValidateProductHandler()
        {
            var request = new UpdateProductRequest(ProductBuilder.New(faker).WithId(product.Id).Build());
            await validateProductHandler.ProcessRequest(request);
            notificationService.HasNotifications.Should().BeFalse();
        }
    }
}