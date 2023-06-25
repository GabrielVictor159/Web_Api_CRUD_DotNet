using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Infraestructure.Database.Repositories
{
    [UseAutofacTestFramework]
    public class ProductRepositoryTest
    {
        private readonly Faker faker;
        private readonly Context context;
        private readonly IProductRepository repository; 
        private readonly IMapper mapper;
        public ProductRepositoryTest(
            Context context,
            Faker faker,
            IProductRepository repository,
            IMapper mapper)
        {
            this.context = context;
            this.faker = faker;
            this.repository = repository;
            this.mapper = mapper;
        }
        [Fact]
        public async Task ShouldNotBeNullByCreateAsync()
        {
            var product = ProductBuilder.New(faker).Build();
            var result = await repository.CreateAsync(product);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldBeTrueByDeleteAsync()
        {
            var product = ProductBuilder.New(faker).Build();
            var productPersist = mapper.Map<infrastructure.Database.Entities.Product>(product);
            await context.Products.AddAsync(productPersist);
            await context.SaveChangesAsync();
            var result = await repository.DeleteAsync(productPersist.Id);
            result.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldNotBeNullOrEmptyByGetOrderAsync()
        {
            var product = ProductBuilder.New(faker).Build();
            var productPersist = mapper.Map<infrastructure.Database.Entities.Product>(product);
            await context.Products.AddAsync(productPersist);
            await context.SaveChangesAsync();
            var result = await repository.GetOrderAsync(e=>e.Id==productPersist.Id,1,10);
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task ShouldAttributesNotBeEntityPersistByUpdateAsync()
        {
            var product = ProductBuilder.New(faker).Build();
            var productPersist = mapper.Map<infrastructure.Database.Entities.Product>(product);
            await context.Products.AddAsync(productPersist);
            await context.SaveChangesAsync();
            var newProduct = ProductBuilder.New(faker).WithId(productPersist.Id).Build();
            var result = await repository.UpdateAsync(newProduct);
            result?.Name.Should().NotBe(product.Name);
        }
    }
}