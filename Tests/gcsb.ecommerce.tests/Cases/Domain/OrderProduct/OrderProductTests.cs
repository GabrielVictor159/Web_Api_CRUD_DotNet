using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.tests.Builder.Domain.OrderProduct;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Domain.OrderProduct
{
    [UseAutofacTestFramework]
    public class OrderProductTests
    {
       private readonly Faker faker;
        public OrderProductTests(Faker faker)
        {
            this.faker = faker;
        } 
        [Fact]
        public void ShouldCreateDomainWithSucces()
        {
            var orderProduct = OrderProductBuilder.New(faker).Build();
            orderProduct.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldNotBeIdEmpty()
        {
            var orderProduct = OrderProductBuilder.New(faker).WithId(Guid.Empty).Build();
            orderProduct.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldNotBeAmount0()
        {
            var orderProduct = OrderProductBuilder.New(faker).WithAmount(0).Build();
            orderProduct.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldNotBeProductInvalid()
        {
            var orderProduct = OrderProductBuilder.New(faker).WithProduct(new domain.Product.Product("",0)).Build();
            orderProduct.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldNotBeIdOrderEmpty()
        {
            var orderProduct = OrderProductBuilder.New(faker).WithIdOrder(Guid.Empty).Build();
            orderProduct.IsValid.Should().BeFalse();
        }
    }
}