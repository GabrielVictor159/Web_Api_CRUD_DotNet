using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.UseCases.Product.DeleteProduct;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Product.DeleteProduct
{
    [UseAutofacTestFramework]
    public class DeleteProductUseCaseTest
    {
        private readonly Faker faker;
        private readonly DeleteProductUseCase deleteProductUseCase;
        private readonly IProductRepository productRepository;
         public DeleteProductUseCaseTest(
            Faker faker,
            DeleteProductUseCase deleteProductUseCase,  
            IProductRepository productRepository)
        {
            this.faker = faker;
            this.deleteProductUseCase = deleteProductUseCase;
            this.productRepository = productRepository;
        }
        [Fact]
        public async Task ShouldOutputSucessBeTrueOutputByDeleteProductUseCase()
        {
            var product = ProductBuilder.New(faker).Build();
            var productPersist = await productRepository.CreateAsync(product);
            var request = new DeleteProductRequest(productPersist.Id);
            await deleteProductUseCase.Execute(request);
            request.deleteOutput!.Sucess.Should().BeTrue();
        }
        [Fact]
        public async Task ShouldOutputSucessBeFalseByDeleteProductUseCaseInvalidRequest()
        {
            var request = new DeleteProductRequest(Guid.NewGuid());
            await deleteProductUseCase.Execute(request);
            request.deleteOutput!.Sucess.Should().BeFalse();
        }
        
    }
}