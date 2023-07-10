using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Domain.Client
{
    [UseAutofacTestFramework]
    public class ClientTests
    {
        private readonly Faker faker;
        public ClientTests(Faker faker)
        {
            this.faker = faker;
        }
        [Fact]
        public void ShouldIsValidBeTrueCreateDomain()
        {
            domain.Client.Client client = ClientBuilder.New(faker).Build();
            client.IsValid.Should().BeTrue();
        }
        [Fact]
        public void ShouldIsValidBeFalseByIdEmpty()
        {
            domain.Client.Client client = ClientBuilder.New(faker).WithId(Guid.Empty).Build();
            client.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByNameLessThan8()
        {
          domain.Client.Client client = ClientBuilder.New(faker).WithName(faker.Random.String2(7)).Build();
          client.IsValid.Should().BeFalse();  
        }
        [Fact]
        public void ShouldIsValidBeFalseByPasswordLessThan8()
        {
           domain.Client.Client client = ClientBuilder.New(faker).WithPassword(faker.Random.String2(7)).Build();
           client.IsValid.Should().BeFalse();
        }
        [Fact]
        public void ShouldIsValidBeFalseByRoleAleatoryString()
        {
            domain.Client.Client client = ClientBuilder.New(faker).WithRole(faker.Random.String2(7)).Build();
            client.IsValid.Should().BeFalse();  
        }
    }
}