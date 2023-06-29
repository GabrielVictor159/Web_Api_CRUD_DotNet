using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.UseCases.Product.CreateProduct;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.CreateProduct
{
    [UseAutofacTestFramework]
    public class CreateProductUseCaseTest
    {
        private readonly Faker faker;
         private readonly CreateProductUseCase createProductUseCase;
         public CreateProductUseCaseTest(Faker faker,CreateProductUseCase createProductUseCase)
         {
            this.faker = faker;
            this.createProductUseCase = createProductUseCase;
         }
         [Fact]
         public async Task ShouldNotBeNullOutputByCreateProductUseCase()
         {
            var request = new CreateProductRequest(ProductBuilder.New(faker).Build());
            await createProductUseCase.Execute(request);
            request.productOutput.Should().NotBeNull();
         }
         [Fact]
         public async Task ShouldBeNullOutputByCreateProductUseCaseInvalidDomain()
         {
            var request = new CreateProductRequest(ProductBuilder.New(faker).WithId(Guid.Empty).Build());
            await createProductUseCase.Execute(request);
            request.productOutput.Should().BeNull();
         }
    }
}