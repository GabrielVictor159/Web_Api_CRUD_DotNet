using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Boundaries.Product;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using gcsb.ecommerce.webapi.UseCases.Product.GetProducts;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.WebApi.UseCases.Product.GetProducts
{
    [UseAutofacTestFramework]
    [Collection("ProductController")]
    public class ProductControllerTest
    {
         private readonly Faker faker;
         private readonly ProductController controller;
         private List<domain.Product.Product> products = new();
         public ProductControllerTest(
            Faker faker,
            ProductController controller,
            IProductRepository productRepository
         )
         {
            this.faker = faker;
            this.controller = controller;
            for(int i = 0; i<5;i++)
            {
                products.Add(ProductBuilder.New(faker).Build());
            }
            foreach(var product in products)
            {
                productRepository.CreateAsync(product).Wait();
            }
         }
         [Fact]
         public async Task ShouldObjectResponseNotBeNullOrEmptyByGetProducts()
         {
             var request = new GetProductsRequest();
             var response = await controller.GetProducts(request) as OkObjectResult;
             var objectResponse = (response?.Value as dynamic)?.Products as List<ProductOutput>;
             objectResponse.Should().NotBeNullOrEmpty();
         }
         [Fact]
         public async Task ShouldObjectResponseCountBe1OrEmptyByGetProductsEspecificId()
         {
             var request = new GetProductsRequest(){Id=products[0].Id.ToString()};
             var response = await controller.GetProducts(request) as OkObjectResult;
             var objectResponse = (response?.Value as dynamic)?.Products as List<ProductOutput>;
             objectResponse!.Count.Should().Be(1);
         }
    }
}