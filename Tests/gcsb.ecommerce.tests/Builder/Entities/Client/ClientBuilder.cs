using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using gcsb.ecommerce.domain.Client.Cryptography;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.infrastructure.Database;

namespace gcsb.ecommerce.tests.Builder.Entities.Client
{
    public class ClientBuilder
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Password { get; private set; }
        public string? Role { get; private set; }
        public dynamic? Context { get; private set; }

        public static ClientBuilder New(Faker faker, dynamic context)
        {
            return new ClientBuilder()
                .WithContext(context)
                .WithId(Guid.NewGuid())
                .WithName(faker.Random.String2(10))
                .WithPassword(faker.Random.String2(10))
                .WithRole(faker.Random.Enum<Policies>().ToString());
        }

        public ClientBuilder WithContext(dynamic context)
        {
            Context = context;
            return this;
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
            Password = Cryptography.md5Hash(password);
            return this;
        }

        public ClientBuilder WithRole(string role)
        {
            Role = role;
            return this;
        }

        public async Task<infrastructure.Database.Entities.Client> Build()
        {
            var entity = new infrastructure.Database.Entities.Client()
            {
                Id = Id,
                Name = Name,
                Password = Password,
                Role = Role
            };

            if (Context != null)
            {
                await Context.AddAsync(entity);
                await Context.SaveChangesAsync();
            }

            return entity;
        }
    }
}