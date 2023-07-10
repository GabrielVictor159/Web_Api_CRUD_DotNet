using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.webapi.UseCases.Product.CreateProduct;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Product.CreateProduct
{
    [UseAutofacTestFramework]
    [Collection("ProductController")]
    public class ProductControllerTest
    {
        private readonly Faker faker;
        private readonly ProductController controller;
        public ProductControllerTest(
            Faker faker,
            IHttpContextMethods httpContextMethods,
            ProductController controller)
            {
                this.faker = faker;
                this.controller = controller;
                var claims = new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,Policies.ADMIN.ToString())

            };
            httpContextMethods.SetHttpContextWithClaims(claims,this.controller);
            }
        [Fact]
        public async Task ShouldObjectResponseNotBeNullByCreateProduct()
        {
            var request = new CreateProductRequest(){Name=faker.Random.String2(8),Value=faker.Random.Decimal(1,100)};
            var response = await controller.CreateProduct(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic)?.product as CreateProductOutput;
            objectResponse.Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldObjectResponseBeNullByCreateProductInvalidDomain()
        {
            var request = new CreateProductRequest(){Name=faker.Random.String2(2),Value=faker.Random.Decimal(1,100)};
            var response = await controller.CreateProduct(request) as OkObjectResult;
            var objectResponse = (response?.Value as dynamic)?.product as CreateProductOutput;
            objectResponse.Should().BeNull();
        }
    }
}