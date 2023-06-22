using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using gcsb.ecommerce.domain.Enums;

namespace gcsb.ecommerce.tests.Builder.Domain.Client
{
    public class ClientBuilder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = "";
        public string Password { get; private set; } = "";
        public string Role { get; private set; } = "";
        public static ClientBuilder New(Faker faker)
        {
            return new ClientBuilder()
                .WithName(faker.Random.String2(10))
                .WithPassword(faker.Random.String2(10))
                .WithRole(faker.Random.Enum<Policies>().ToString());
        }
        public ClientBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public ClientBuilder WithName(string name)
        {
            Name = name;
            return this;
        }
        public ClientBuilder WithPassword(string password)
        {
            Password = password;
            return this;
        }
        public ClientBuilder WithRole(string role)
        {
            Role = role;
            return this;
        }
        public domain.Client.Client Build()
            => new domain.Client.Client(Id, Name, Password, Role);
    }
}
