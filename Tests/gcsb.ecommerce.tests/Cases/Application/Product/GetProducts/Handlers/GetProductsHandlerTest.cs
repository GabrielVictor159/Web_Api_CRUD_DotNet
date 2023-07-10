using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.UseCases.Product.GetProducts;
using gcsb.ecommerce.application.UseCases.Product.GetProducts.Handlers;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.GetProducts.Handlers
{
    [UseAutofacTestFramework]
    public class GetProductsHandlerTest
    {
        private readonly Faker faker;
        private readonly GetProductsHandler getProductsHandler;
        private List<domain.Product.Product> products = new();
        public GetProductsHandlerTest(
            Faker faker, 
            GetProductsHandler getProductsHandler,
            IProductRepository productRepository)
        {
            this.faker = faker;
            this.getProductsHandler = getProductsHandler;
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
        public async Task ShouldProductResultNotBeNullByGetProductsHandler()
        {
           Expression<Func<domain.Product.Product, bool>> func = p =>true;
            var request = new GetProductsRequest(func,1,10);
            await getProductsHandler.ProcessRequest(request);
            request.productsResult.Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldProductResultBeNullOrEmptyByGetProductsHandlerEspecificId()
        {
           Expression<Func<domain.Product.Product, bool>> func = p =>p.Id==Guid.NewGuid();
            var request = new GetProductsRequest(func,1,10);
            await getProductsHandler.ProcessRequest(request);
            request.productsResult.Should().BeNullOrEmpty();
        }
    }
}