using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using gcsb.ecommerce.webapi.UseCases.Product.UpdateProducts;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Product.UpdateProduct
{
    [UseAutofacTestFramework]
    [Collection("ProductController")]
    public class ProductControllerTest
    {
         private readonly Faker faker;
         private readonly ProductController controller;
         public domain.Product.Product product;
         public ProductControllerTest(
            Faker faker, 
            ProductController controller, 
            IProductRepository productRepository)
            {
                this.faker = faker;
                this.controller = controller;
                product = ProductBuilder.New(faker).Build();
                productRepository.CreateAsync(product).Wait();
            }
            [Fact]
            public async Task ShouldObjectResponseBeNullByUpdateProductInvalidId()
            {
                var request = new UpdateProductRequest(){Id=Guid.NewGuid(),Name = faker.Random.String2(8), Value = faker.Random.Decimal(1,100)};
                var response = await controller.UpdateProduct(request) as OkObjectResult;
                var objectResponse = (response?.Value as dynamic)?.Product as UpdateProductOutput;
                objectResponse.Should().BeNull();
            }
            [Fact]
            public async Task ShouldObjectResponseNotBeNullAndAttributesBeRequestByUpdateProduct()
            {
                var request = new UpdateProductRequest(){Id=product.Id,Name = faker.Random.String2(8), Value = faker.Random.Decimal(1,100)};
                var response = await controller.UpdateProduct(request) as OkObjectResult;
                var objectResponse = (response?.Value as dynamic)?.Product as UpdateProductOutput;
                objectResponse.Should().NotBeNull();
                objectResponse!.Name.Should().Be(request.Name);
                objectResponse!.Value.Should().Be(request.Value);
            }
    }
}