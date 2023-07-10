using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.UseCases.Product.DeleteProduct;
using gcsb.ecommerce.application.UseCases.Product.DeleteProduct.Handlers;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.DeleteProduct.Handlers
{
    [UseAutofacTestFramework]
    public class DeleteProductHandlerTest
    {
        private readonly Faker faker;
        private readonly DeleteProductHandler deleteProductHandler;
        private readonly Context context;
        private readonly IProductRepository productRepository;
        public DeleteProductHandlerTest(
            Faker faker, 
            DeleteProductHandler deleteProductHandler, 
            Context context,
            IProductRepository productRepository)
        {
            this.faker = faker;
            this.deleteProductHandler = deleteProductHandler;
            this.context = context;
            this.productRepository = productRepository;
        }
         [Fact]
        public async Task ShouldBeNullSearchProductAfterDeleteProductHandler()
        {
            var product = ProductBuilder.New(faker).Build();
            await productRepository.CreateAsync(product);
            var request = new DeleteProductRequest(product.Id);
            await deleteProductHandler.ProcessRequest(request);
            var result = context.Products.Find(request.ProductId);
            result.Should().BeNull();
        }

    }
}