using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Domain.Product
{
     [UseAutofacTestFramework]
    public class ProductTests
    {
        private readonly Faker faker;
        public ProductTests(Faker faker)
        {
            this.faker = faker;
        }
        [Fact]
        public void ShouldIsValidBeTrueByCreateDomainWithSucces()
        {
            var product = ProductBuilder.New(faker).Build();
            product.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldIsValidBeFalseByIdEmpty()
        {
            var product = ProductBuilder.New(faker).WithId(Guid.Empty).Build();
            product.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByNameEmpty()
        {
            var product = ProductBuilder.New(faker).WithName("").Build();
            product.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByValue0()
        {
            var product = ProductBuilder.New(faker).WithValue(0).Build();
            product.IsValid.Should().BeFalse();  
        }

    }
}