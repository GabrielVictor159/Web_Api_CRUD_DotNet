using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using gcsb.ecommerce.webapi.UseCases.Product.DeleteProducts;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Product.DeleteProduct
{
    [UseAutofacTestFramework]
    [Collection("ProductController")]
    public class ProductControllerTest
    {
        private readonly Faker faker;
        private readonly ProductController controller;
        private domain.Product.Product product;
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
            public async Task ShouldObjectResponseSucessBeFalseByDeleteProductInvalidId()
            {
            var request = new DeleteProductRequest(){ProductId=Guid.NewGuid()};
            var response = await controller.DeleteProduct(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic)?.DeleteRequest as DeleteProductOutput;
            objectResponse!.Sucess.Should().BeFalse();
            }
            [Fact]
            public async Task ShouldObjectResponseSucessBeTrueByDeleteProduct()
            {
            var request = new DeleteProductRequest(){ProductId=product.Id};
            var response = await controller.DeleteProduct(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic)?.DeleteRequest as DeleteProductOutput;
            objectResponse!.Sucess.Should().BeTrue();
            }
    }
}