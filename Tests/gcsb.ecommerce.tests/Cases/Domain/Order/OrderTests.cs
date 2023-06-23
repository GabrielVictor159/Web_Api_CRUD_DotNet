using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.tests.Builder.Domain;
using gcsb.ecommerce.tests.Builder.Domain.Order;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Domain.Order
{
    [UseAutofacTestFramework]
    public class OrderTests
    {
        private readonly Faker faker;
        public OrderTests(Faker faker)
        {
            this.faker = faker;
        }
        [Fact]
        public void ShouldIsValidBeTrueByCreateDomain()
        {
            var order = OrderBuilder.New(faker).Build();
            order.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldIsValidBeFalseByIdEmpty()
        {
            var order = OrderBuilder.New(faker).WithId(Guid.Empty).Build();
            order.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByIdClientEmpty()
        {
            var order = OrderBuilder.New(faker).WithIdClient(Guid.Empty).Build();
            order.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByListEmpty()
        {
            var order = OrderBuilder.New(faker).WithOrderProducts(new List<domain.OrderProduct.OrderProduct>()).Build();
            order.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByOrderDateByFuture()
        {
            var order = OrderBuilder.New(faker).WithOrderDate(DateTime.UtcNow.AddDays(2)).WithOrderProducts(new List<domain.OrderProduct.OrderProduct>()).Build();
            order.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByCupomAleatoryString()
        {
            var order = OrderBuilder.New(faker).WithNameCupom(faker.Random.String2(7)).Build();
            order.IsValid.Should().BeFalse();
        }
    }
}