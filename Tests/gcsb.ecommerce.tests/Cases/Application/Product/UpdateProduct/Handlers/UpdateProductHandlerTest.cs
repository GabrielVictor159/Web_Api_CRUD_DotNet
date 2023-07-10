using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.UseCases.Product.UpdateProduct;
using gcsb.ecommerce.application.UseCases.Product.UpdateProduct.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.UpdateProduct.Handlers
{
    [UseAutofacTestFramework]
    public class UpdateProductHandlerTest
    {
         private readonly Faker faker;
         private domain.Product.Product product;
         private readonly UpdateProductHandler updateProductHandler;
         private readonly Context context;
         public UpdateProductHandlerTest(
            Faker faker,
            UpdateProductHandler updateProductHandler,
            Context context,
            IProductRepository productRepository
         )
         {
            this.faker = faker;
            this.updateProductHandler = updateProductHandler;
            this.context = context;
            product = ProductBuilder.New(faker).Build();
            productRepository.CreateAsync(product).Wait();
         }
         [Fact]
         public async Task ShouldProductSaveBeProductRequestByUpdateProductHandler()
         {
            var request = new UpdateProductRequest(ProductBuilder.New(faker).WithId(product.Id).Build());
            await updateProductHandler.ProcessRequest(request);
            var result = context.Products.Find(product.Id);
            result!.Name.Should().Be(request.Product.Name);
         }
    }
}