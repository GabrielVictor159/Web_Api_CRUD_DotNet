using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.UseCases.Product.CreateProduct;
using gcsb.ecommerce.application.UseCases.Product.CreateProduct.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.CreateProduct.Handlers
{
    [UseAutofacTestFramework]
    public class SaveProductHandlerTest
    {
        private readonly Faker faker;
        private readonly SaveProductHandler saveProductHandler;
        private readonly Context context;
        public SaveProductHandlerTest(
            Faker faker, 
            SaveProductHandler saveProductHandler, 
            Context context)
        {
            this.faker = faker;
            this.saveProductHandler = saveProductHandler;
            this.context = context;
        }
        [Fact]
        public async Task ShouldNotBeNullSaveProductsBySaveProductHandler()
        {
            var request = new CreateProductRequest(ProductBuilder.New(faker).Build());
            await saveProductHandler.ProcessRequest(request);
            var result = context.Products.Find(request.Product.Id);
            result.Should().NotBeNull();
        }
    }
}